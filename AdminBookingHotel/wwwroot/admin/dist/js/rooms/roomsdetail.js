$(document).ready(function () {
    $('.selectpicker').selectpicker();
    GetSeletedRoomType();
    GetSeletedRoomHuman();

    var strType = $("#RoomTypeString").val();
    var resultType = strType.split(",");
    $("#modelRoomType").selectpicker('val', resultType);

    var strHuman = $("#RoomHumanString").val();
    var resultHuman = strHuman.split(",");
    $("#modelRoomHuman").selectpicker('val', resultHuman);
});

function GetSeletedRoomType() {
    $('#modelRoomType').append("<option value='1 giường đôi'>1 giường đôi</option>");
    $('#modelRoomType').append("<option value='2 giường đơn'>2 giường đơn</option>");
    $('#modelRoomType').append("<option value='2 giường đôi'>2 giường đôi</option>");
    $('#modelRoomType').append("<option value='1 giường đôi hoặc 2 giường đơn'>1 giường đôi hoặc 2 giường đơn</option>");
    $('#modelRoomType').append("<option value='2 giường đơn / 1 giường King'>2 giường đơn / 1 giường King</option>");
    $('#modelRoomType').append("<option value='2 giường đơn / 1 giường Queen'>2 giường đơn / 1 giường Queen</option>");
    $('#modelRoomType').append("<option value='1 giường đôi + 1 giường tầng'>1 giường đôi + 1 giường tầng</option>");
    $('#modelRoomType').selectpicker('refresh');
}

function GetSeletedRoomHuman() {
    $('#modelRoomHuman').append("<option value='2 người lớn'>2 người lớn</option>");
    $('#modelRoomHuman').append("<option value='2 em bé'>2 em bé</option>");
    $('#modelRoomHuman').append("<option value='1 em bé'>1 em bé</option>");
    $('#modelRoomHuman').append("<option value='2 bé dưới 6 tuổi'>2 bé dưới 6 tuổi</option>");
    $('#modelRoomHuman').append("<option value='1 bé dưới 12 tuổi'>1 bé dưới 12 tuổi</option>");
    $('#modelRoomHuman').selectpicker('refresh');
}

$("#updateBtn").click(function () {

    if ($("#modelId").val() == "") { id = 0 }
    else { id = $("#modelId").val() }
    var modelRoomName = $("#modelRoomName").val();
    var modelRoomType = ($('#modelRoomType').val() != null ? $('#modelRoomType').val().join(',') : "");
    var modelRoomHuman = ($('#modelRoomHuman').val() != null ? $('#modelRoomHuman').val().join(',') : "");
    var modelPrice = parseFloat($("#modelPrice").val());
    var modelRoomSize = $("#modelRoomSize").val();
    var modelDescription = $("#modelDescription").val();
    var imgCodeByUserId = $("#imgCodeByUserId").val();
    const modelActiveCheckbox = document.getElementById('modelActive');
    const modelActive = modelActiveCheckbox.checked;

    model = {
        Id: id, RoomName: modelRoomName
        , RoomType: modelRoomType, RoomHuman: modelRoomHuman, Price: modelPrice, Active: modelActive
        , RoomSize: modelRoomSize, Description: modelDescription, ImgCode: imageData, ImgCodeByUserId: imgCodeByUserId
    }

    $.ajax({
        url: '/WebRooms/Update',
        type: 'POST',
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $.blockUI({
                message: '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-loader spin ml-2"><line x1="12" y1="2" x2="12" y2="6"></line><line x1="12" y1="18" x2="12" y2="22"></line><line x1="4.93" y1="4.93" x2="7.76" y2="7.76"></line><line x1="16.24" y1="16.24" x2="19.07" y2="19.07"></line><line x1="2" y1="12" x2="6" y2="12"></line><line x1="18" y1="12" x2="22" y2="12"></line><line x1="4.93" y1="19.07" x2="7.76" y2="16.24"></line><line x1="16.24" y1="7.76" x2="19.07" y2="4.93"></line></svg>',
                fadeIn: 800,
                overlayCSS: {
                    backgroundColor: '#1b2024',
                    opacity: 0.8,
                    zIndex: 1200,
                    cursor: 'wait'
                },
                css: {
                    border: 0,
                    color: '#fff',
                    zIndex: 1201,
                    padding: 0,
                    backgroundColor: 'transparent'
                }
            });
        },
        complete: function () {
            $.unblockUI();
        },
        success: function (response) {
            Swal.fire({
                icon: 'success',
                title: 'OK',
                text: 'Cập nhập thông tin thành công'
            });
        },
        error: function (response) {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi',
                text: 'Cập nhập thông tin không thành công'
            });
        }
    })
});
$("#uploadImg").click(function () {
    convertImg();
    //convertImg().then(d => { console.log(d); imageData = d; });
})
function convertImg() {
    var input = document.getElementById('featureImg');
    var file = input.files[0];
    var imgData;
    var result = new Promise((resolve, reject) => {
        if (file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var base64String = e.target.result.split(",")[1];
                sendDataToApi(base64String).done(function (d) {
                    resolve(d);
                })

            }
            reader.readAsDataURL(file);
            $("#imagePreview").attr("src", URL.createObjectURL(file));
        } else {
            reject(undefined);
        }
    });
    return result;
}
function sendDataToApi(base64String) {
    var sign = crypto.randomUUID();
    var apiUrl = "WebHotels/UploadImage";
    var returnData;
    return $.ajax({
        type: "POST",
        url: apiUrl,
        data: {
            sign: sign,
            base64Image: base64String
        },

        success: function (response) {

            var obj = JSON.parse(response);
            returnData = obj.description;
            imageData = returnData;
 /*           console.log("imageData:" + imageData);*/
        },
        error: function (error) {
            console.log(error);
        }
    });
}
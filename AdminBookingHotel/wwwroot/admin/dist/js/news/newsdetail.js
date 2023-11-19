$(document).ready(function () {
    $('.selectpicker').selectpicker();
    var imageData;
});

$("#updateBtn").click(function () {

    if ($("#modelId").val() == "") { id = 0 }
    else { id = $("#modelId").val() }
    var modelTitle = $("#modelTitle").val();
    var modelSumContent = $("#modelSumContent").val();
    var modelNewsContent = $("#modelNewsContent").val();
    var modelSource = $("#modelSource").val();

    const modelActiveCheckbox = document.getElementById('modelActive');
    const modelActive = modelActiveCheckbox.checked;

    model = {
        Id: id, Title: modelTitle, SumContent: modelSumContent,
        NewsContent: modelNewsContent, Source: modelSource, Active: modelActive, ImgCodeByUserId: imageData
    }
    $.ajax({
        url: 'WebNews/Update',
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
    debugger
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

//function sendDataToApi(base64String) {
//    var sign = crypto.randomUUID();
//    var apiUrl = "https://localhost:7219/api/Identity/UploadImage";
//    return $.ajax({
//        xhrFields: {
//            withCredentials: true
//        },
//        crossDomain: true,
//        type: "POST",
//        url: apiUrl,
//        data: {
//            sign: sign,
//            base64Image: base64String
//        },

//        success: function (response) {

//            var obj = JSON.parse(response);
//            returnData = obj.description;
//            console.log(returnData)
//        },
//        error: function (error) {
//            console.log(error);
//        }
//    });
//}

function sendDataToApi(base64String) {
    var sign = crypto.randomUUID();
    var apiUrl = "WebNews/UploadImage";
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
            console.log("imageData:" + imageData);
        },
        error: function (error) {
            console.log(error);
        }
    });
}
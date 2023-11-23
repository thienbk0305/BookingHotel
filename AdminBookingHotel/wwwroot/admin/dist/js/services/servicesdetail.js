$(document).ready(function () {
    $('.selectpicker').selectpicker();

    GetSeletedService();
    $("#modelServiceType").change(function () {
        $('#modelServiceContent').empty();
        $('#modelServiceContent').attr('title', 'Chọn Chi Tiết Dịch Vụ');
        GetSeletedService();
    });
    var str = $("#ServiceContentString").val();
    var result = str.split(",");
    $("#modelServiceContent").selectpicker('val', result);
});

function GetSeletedService() {
    if ($("#modelServiceType").val() == "0") {
        $('#modelServiceContent').append("<option value='Hồ bơi ngoài trời (quanh năm)'>Hồ bơi ngoài trời (quanh năm)</option>");
        $('#modelServiceContent').append("<option value='Trung tâm thể dục'>Trung tâm thể dục</option>");
        $('#modelServiceContent').append("<option value='Khu vực thư giãn/spa lounge'>Khu vực thư giãn/spa lounge</option>");
        $('#modelServiceContent').append("<option value='Nhà hàng'>Nhà hàng</option>");
        $('#modelServiceContent').append("<option value='Quầy bar'>Wifi</option>");
        $('#modelServiceContent').append("<option value='Máy lạnh'>Máy lạnh</option>");
        $('#modelServiceContent').append("<option value='Giáp biển'>Giáp biển</option>"); //*
        $('#modelServiceContent').append("<option value='Tiện nghi tổ chức tiệc/họp'>Tiện nghi tổ chức tiệc/họp</option>"); //*
        $('#modelServiceContent').append("<option value='Phòng không hút thuốc'>Phòng không hút thuốc</option>");
    } else if ($("#modelServiceType").val() == "1") {
        $('#modelServiceContent').append("<option value='“Service & WINE”/ giá phòng có rượu'>“Service & WINE”/ giá phòng có rượu</option>");
        $('#modelServiceContent').append("<option value='Giá trên đã bao gồm thuế, phí dịch vụ và ăn sáng cho 02 người lớn và 02 trẻ em dưới 06 tuổi ngủ chung với bố mẹ'>Giá trên đã bao gồm thuế, phí dịch vụ và ăn sáng cho 02 người lớn và 02 trẻ em dưới 06 tuổi ngủ chung với bố mẹ</option>");
        $('#modelServiceContent').append("<option value='Miễn phí sử dụng internet tốc độ cao'>Miễn phí sử dụng hồ bơi, Gym</option>");
        $('#modelServiceContent').append("<option value='Chỗ đậu xe'>Chỗ đậu xe</option>");
        $('#modelServiceContent').append("<option value='Nôi trẻ em miễn phí'>Nôi trẻ em miễn phí</option>");//*
        $('#modelServiceContent').append("<option value='Buffet sáng theo chuẩn quốc tế'>Buffet sáng theo chuẩn quốc tế</option>");//*
        $('#modelServiceContent').append("<option value='Nước uống chào mừng khi nhận phòng'>Nước uống chào mừng khi nhận phòng</option>");
        $('#modelServiceContent').append("<option value='01 chai rượu vang nhập khẩu từ Úc mỗi phòng áp dụng cho giá phòng “Service & WINE”'>01 chai rượu vang nhập khẩu từ Úc mỗi phòng áp dụng cho giá phòng “Service & WINE”</option>");

    } else {
        $('#modelServiceContent').append("<option value='Máy lạnh'>Máy lạnh</option>");
        $('#modelServiceContent').append("<option value='Thang máy'>Thang máy</option>");
        $('#modelServiceContent').append("<option value='Phòng gia đình'>Phòng gia đình</option>");
        $('#modelServiceContent').append("<option value='Cà phê/Trà tại sảnh'>Cà phê/Trà tại sảnh</option>");
        $('#modelServiceContent').append("<option value='Tiệm làm đẹp'>Tiệm làm đẹp</option>");
        $('#modelServiceContent').append("<option value='Wifi'>Wifi</option>");
        $('#modelServiceContent').append("<option value='Phòng không hút thuốc'>Phòng không hút thuốc</option>");
        $('#modelServiceContent').append("<option value='Dịch vụ phòng'>Dịch vụ phòng</option>");
    }

    $('#modelServiceContent').selectpicker('refresh');
}

$("#updateBtn").click(function () {

    if ($("#modelId").val() == "") { id = 0 }
    else { id = $("#modelId").val() }
    var modelServiceName = $("#modelServiceName").val();
    var modelServiceType = $('#modelServiceType').val();
    var modelServiceContent = ($('#modelServiceContent').val() != null ? $('#modelServiceContent').val().join(',') : "");
    var modelDescription = $("#modelDescription").val();

    const modelActiveCheckbox = document.getElementById('modelActive');
    const modelActive = modelActiveCheckbox.checked;


    model = {
        Id: id, ServiceName: modelServiceName, ServiceType: modelServiceType, ServiceContent: modelServiceContent,
        Active: modelActive, Description: modelDescription
    }

    $.ajax({
        url: '/WebServices/Update',
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
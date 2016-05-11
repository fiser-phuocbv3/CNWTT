var currentPage = 1;

function loadMap() {

    var mapOptions = {
        center:new google.maps.LatLng(19, 78),
        zoom:10,
        // mapTypeId:google.maps.MapTypeId.ROADMAP
    };

    var map = new google.maps.Map(document.getElementById("sample"),mapOptions);

    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(17.088291, 78.442383),
        map: map,
    });
}

jQuery(document).ready(function ($) {
    var pageCount = parseInt($('#pageCount').val());

    $('#first').click(function () {   
        alert(pageCount);
        if (currentPage > 0) {
            if (currentPage > 1) {
                currentPage--;
                loadPage(currentPage);     
            }
            
        }
        alert(currentPage);
    });

    $('#last').click(function () {
        if (currentPage < pageCount) {
            currentPage++;
            loadPage(currentPage);     
        }
        alert(currentPage);
    });


});

function loadPage(page) {
    $.ajax({
        url: '/Home/getListPage',
        type: 'POST',
        dataType: 'html',
        data: { page: page },
    })
    .done(function (data) {
        console.log("success");
        if (data != "") {
            $('#displayProduct').html(data);

        } else {
            alert("Không có dữ liệu");
        }
    })
    .fail(function () {
        console.log("error");
    })
    .always(function () {
        console.log("complete");
    });
}

//form validate
jQuery(document).ready(function ($) {
    $('#formRegister').validate({
        rules: {
            name:{
                minlength: 10,
            },
            userName: {
                minlength: 6,
                remote: {
                    url: "/Home/checkUserName",
                    type: "POST",
                }
            },
            password: {
                minlength: 6,

            },
            confirmPassword: {
                minlength: 6,
                equalTo:"#password",
            },
            identify: {
                minlength: 8,
                number: true,
            },
            phone: {
                number: true,
                minlength: 10,
            },
            email: {
                email: true,
            }
        },
        messages: {
            name: {
                minlength:"Nhập tối thiểu 10 kí tự",
            },
            userName:{
                minlength: "Nhập tối thiểu 6 kí tự",
                remote: "Tên tài khoản đã được sử dụng",
            },
            password: {
                minlength:"Mật khẩu tối thiểu 6 kí tự",
            },
            confirmPassword: {
                minlength: "Nhập tối thiểu 6 kí tự",
                equalTo: "Mật khẩu nhập lại không trùng với mật khẩu trước",
            },
            identify: {
                minlength: "Nhập tối thiểu 8 kí tự",
                number:"Giá trị phải là một số",
            },
            phone: {
                number: "Giá trị phải là một số",
                minlength:"Nhập tối thiểu 10 kí tự",
            },
            email:{
                email:"Không phải email",
            }
        }
    });

    $('#formFeedBack').validate({
        rules: {
            content: {
                minlength: 100,
            }
        },
        messages: {
            content: {
                minlength:"Tối thiểu 100 kí tự",
            }
        }
    });
});
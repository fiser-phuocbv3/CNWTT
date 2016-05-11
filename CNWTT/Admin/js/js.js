jQuery(document).ready(function ($) {
    $("#addAccountAdmin").validate({
        rules: {
            name: {           
                minlength:10,
            },
            userName: {            
                minlength: 6,
                remote: {
                    url: "/Admin/checkUserName",
                    type:"POST",
                }
            },
            password: {             
                minlength: 6,
                maxlength:32,
            },
            confirmPassword: {               
                minlength: 6,
                maxlength:32,
                equalTo:"#password",
            },
            identify:{             
                number: true,
            },
            phone:{           
                number: true,
                minlength: 10,
            },
            address:{             
                minlength: 5,
            },
            email: {         
                email: true,
            },         
        },
        messages: {
            name: {
                requered: "Vui lòng nhập tên người dùng",
                minlength: "Tên người dùng phải dài hơn 10 kí tự",
            },
            userName: {
                requered:"Vui lòng nhập tên tài khoản",
                minlength: "Tên đăng nhập phải lớn hơn 6 kí tự",
                remote:"Tên tài khoản đã trùng, vui lòng nhập tên khác",
            },
            password: {
                requered:"Vui lòng nhập mật khẩu",
                minlength: "Mật khẩu phải dài hơn 6 kí tự",              
            },
            confirmPassword: {
                requered: "Vui lòng nhập lại mật khẩu",
                minlength: "Mật khẩu phải dài hơn 6 kí tự",
                equalTo:"Mật khẩu nhập lại không giống mật khẩu trước",
            },
            email: {
                requered: "Vui lòng nhập email",
                email: "Không đúng định đạng email",
            }
        }
    });

    $("#newManufacturerForm").validate({
        rules: {
            name: {
                remote: {
                    url: "/Admin/checkManufacturer",
                    type:"POST",
                }
            }
        },
        messages: {
            name: {
                remote: "Tên này đã có vui lòng chọn tên khác",
            }
        },
    });
});

function validate() {
    var selectManufacturer = document.formViewProduct.selectManufacturer.value;
    var selectPrice = document.formViewProduct.selectPrice.value;
    if (selectManufacturer == "" || selectPrice == "") {
        alert("Nhập thiếu thông tin!");
        return false;
    }  
    return true;
}



function loadProduct(selectManufacturer, selectPrice) {
    $.ajax({
        url: '/Admin/ViewProduct',
        type: 'POST',
        dataType: 'html',
        data: {
            selectManufacturer: selectManufacturer,
            selectPrice: selectPrice,
            //page: page
        },
    })
    .done(function (data) {
        console.log("success");
        if (data != NaN) {
           

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


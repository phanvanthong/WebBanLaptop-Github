

$('.Add-Cart').off('click').on('click', function (e) {
    e.preventDefault(); //chặn chuyển tới đường dẫn mặc định
    //var product = $(this).data('laptopid'));
    var CartProduct =( {
        iMaSP: $(this).data('laptopid')//Lấy dữ liệu từ thẻ
    });
    $.ajax({ //gửi dữ liệu đi
        url: '/Giohang/AddItem', //đường dẫn tới file xử lý
        data: { cartModel: JSON.stringify(CartProduct) }, //các dữ liệu gửi đi
        dataType: 'json', // truyền kiểu json
        type: 'POST', //phương thức  
        //thực thi success khi dữ liệu gửi đi thành công
        success: function (res) {
            if (res.status == true) {
                debugger
                var soluonggiohang = parseInt($('#soluonggiohang').text())+1;
                $('#soluonggiohang').text(soluonggiohang);
                $('#SuccessMD').modal('show');
                var action = setTimeout(function () {
                    $('#SuccessMD').modal('hide');
                }, 1500);
                $('.modal-backdrop').hide();
            }   
        }
    });
});

$('.Add-CartChiTiet').off('click').on('click', function (e) {
    e.preventDefault();
    //var product = $(this).data('laptopid'));
    var CartProduct = ({
        iMaSP: $(this).data('laptopid'),//Lấy dữ liệu từ thẻ
        iSoLuong: $('#qty').val()
    });
    $.ajax({ //gửi dữ liệu đi
        url: '/Giohang/AddItemChiTiet', //đường dẫn tới file xử lý
        data: { cartModel: JSON.stringify(CartProduct) }, //các dữ liệu gửi đi
        dataType: 'json', //?
        type: 'POST', //phương thức
        //thực thi success khi dữ liệu gửi đi thành công
        success: function (res) {
            if (res.status == true) {
                var soluonggiohang = parseInt($('#soluonggiohang').text()) + parseInt($('#qty').val());
                $('#soluonggiohang').text(soluonggiohang);
                debugger
                $('#SuccessMD').modal('show');
                var action = setTimeout(function () {
                    $('#SuccessMD').modal('hide');
                }, 1500);
                $('.modal-backdrop').hide();
            }
        }
    });
});

$('.MuaNgayChiTiet').off('click').on('click', function (e) {
    e.preventDefault();
    //var product = $(this).data('laptopid'));
    var CartProduct = ({
        iMaSP: $(this).data('laptopid'),//Lấy dữ liệu từ thẻ
        iSoLuong: $('#qty').val()
    });
    $.ajax({ //gửi dữ liệu đi
        url: '/Giohang/MuaNgayChiTiet', //đường dẫn tới file xử lý
        data: { cartModel: JSON.stringify(CartProduct) }, //các dữ liệu gửi đi
        dataType: 'json', //?
        type: 'POST', //phương thức
        success: function (res) {
            window.location= '/GioHang/Index';
        }
    });
});

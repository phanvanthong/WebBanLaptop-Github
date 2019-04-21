$('.Add-Cart').off('click').on('click', function (e) {
    e.preventDefault();
    //var product = $(this).data('laptopid'));
    var CartProduct =( {
        iMaSP: $(this).data('laptopid')//Lấy dữ liệu từ thẻ
    });
    $.ajax({ //gửi dữ liệu đi
        url: '/Giohang/AddItem', //đường dẫn tới file xử lý
        data: { cartModel: JSON.stringify(CartProduct) }, //các dữ liệu gửi đi
        dataType: 'json', //?
        type: 'POST', //phương thức
        //thực thi success khi dữ liệu gửi đi thành công
        //success: function (res) {
        //    if (res.status == true) {
        //        //window.location.href = '/Cart';
        //    }   
        //}
    });
});
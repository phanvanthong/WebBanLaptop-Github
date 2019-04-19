$('.Add-Cart').off('click').on('click', function (e) {
    e.preventDefault();
    //var product = $(this).data('laptopid'));
    var CartProduct =( {
        iMaSP: $(this).data('laptopid')
    });
    $.ajax({
        url: '/Giohang/AddItem',
        data: { cartModel: JSON.stringify(CartProduct) },
        dataType: 'json',
        type: 'POST',
        success: function (res) {
            if (res.status == true) {
                //window.location.href = '/Cart';
            }   
        }
    });
});
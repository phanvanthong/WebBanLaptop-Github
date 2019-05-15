function TDtrangthai(id) {
    var trangthaiClass = "#trangthai_" + id;
    //var trangthai = $(trangthaiClass).val();
    debugger
    var order = ({
        Order_id: id,//Lấy dữ liệu từ thẻ
    });
    $.ajax({ //gửi dữ liệu đi
        url: '/QuanLyDonHang/TrangThaiDonHang', //đường dẫn tới file xử lý
        data: { cartModel: JSON.stringify(order), trangthai }, //các dữ liệu gửi đi
        dataType: 'json', //xác định dữ liệu trả về thuộc dạng nào
        type: 'POST', //phương thức
        //thực thi success khi dữ liệu gửi đi thành công
        success: function (res) {
            if (res.status == true) {
                if (trangthai == 1) window.location = '/QuanLyDonHang/DonHangChuaXacNhan';
                else if (trangthai == 2) window.location = '/QuanLyDonHang/DonHangDangXuLy';
                else if (trangthai == 3) window.location = '/QuanLyDonHang/DonHangDaGiao';
                else if (trangthai == 4) window.location = '/QuanLyDonHang/DonHangBiHuy';
            }
        }
    });
}

//function LoadData(id, trangthai) {
//    $.ajax({
//        type: 'POST',
//        data: { cartModel: JSON.stringify(id), trangthai }, //các dữ liệu gửi đi
//        dateType: 'json',
//        success: function (res) {
//            if (res.status == true) {
//                if (trangthai == 1) window.location = '/QuanLyDonHang/DonHangChuaXacNhan';
//                else if (trangthai == 2) window.location = '/QuanLyDonHang/DonHangDangXuLy';
//                else if (trangthai == 3) window.location = '/QuanLyDonHang/DonHangDaGiao';
//                else if (trangthai == 4) window.location = '/QuanLyDonHang/DonHangBiHuy';
//            }
//        }
//    });

//}


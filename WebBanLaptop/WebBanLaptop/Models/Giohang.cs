using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanLaptop.Models;
namespace WebBanLaptop.Models
{
    public class Giohang
    {
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinhAnh { get; set; }
        public double dKhuyenMai { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien {
            get { return iSoLuong * (dDonGia -dDonGia* dKhuyenMai/100); }
        }
        //Hàm tạo cho giỏ hàng
        //public Giohang(int MaSP)
        //{
        //    iMaSP = MaSP;
        //    Product product = db.Products.SingleOrDefault(n => n.Products_id == iMaSP);
        //    sTenSP = product.Name;
        //    Discount discount = db.Discounts.SingleOrDefault(n => n.Discount_id == product.Discount_id);
        //    dKhuyenMai = (Double)discount.value;
        //    sHinhAnh = "img";
        //    dDonGia = (Double)product.Gia; /*Double.Parse(product.Gia.ToString())*/
        //    iSoLuong = 1;
        //}

        public static explicit operator List<object>(Giohang v)
        {
            throw new NotImplementedException();
        }
    }
}
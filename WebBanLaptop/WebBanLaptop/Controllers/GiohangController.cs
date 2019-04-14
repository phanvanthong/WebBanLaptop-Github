 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;

namespace WebBanLaptop.Controllers
{
    public class GiohangController : Controller
    {
        // GET: Giohang
        //lấy giò hàng
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public List<Giohang> LayGioHang()
        {
            List<Giohang> lstGioHang = Session["GioHang"+Session["DangNhap"]] as List<Giohang>; //ép kiểu session kiểu giỏ hàng
            if (lstGioHang == null)
            {
                lstGioHang = new List<Giohang>();
                Session["GioHang" + Session["DangNhap"]] = lstGioHang;
            }
            return lstGioHang;
        }

        //Thêm giỏ hang
        public ActionResult ThemGioHang(int iMaSP,string strURL )
        {
            if (Session["DangNhap"] == null)
            {
                return RedirectToAction("DangNhap", "Users");
            }
            Product product = db.Products.SingleOrDefault(n => n.Products_id == iMaSP);
            if(product==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<Giohang> lstGioHang = LayGioHang();
            Giohang gh = lstGioHang.Find(n => n.iMaSP == iMaSP);
            if(gh==null)
            {
                gh = new Giohang(iMaSP);
                lstGioHang.Add(gh);
                return Redirect(strURL);
            }
            else
            {
                gh.iSoLuong ++;
                return Redirect(strURL);
            }

        }

        public ActionResult CapnhatGioHang(int iMaSP,FormCollection f)
        {
            //Kiểm tra mã sp
            Product product = db.Products.SingleOrDefault(n=>n.Products_id==iMaSP);
            if(product==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Giohang> lstGioHang = LayGioHang();
            Giohang gh = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if(gh!=null)
            {
                gh.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return View("GioHang");
        }

        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {
            //kiểm tra mã sp
            Product product = db.Products.SingleOrDefault(n => n.Products_id == iMaSP);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng từ session
            List<Giohang> lstGioHang = LayGioHang();
            Giohang gh = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            //Nếu  mà tồn tại thì ta sửa số lượng
            if(gh!=null)
            {
                lstGioHang.RemoveAll(n => n.iMaSP == iMaSP);
            }
            return Redirect("Index");
        }

        public ActionResult Index()
        {
            if (Session["DangNhap"] == null)
            {
                return RedirectToAction("DangNhap", "Users");
            }
            List<Giohang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        private int TongSoLuong()
        {
            int iSoLuong = 0;
            List<Giohang> lstGioHang = Session["GioHang" + Session["DangNhap"]] as List<Giohang>;
            if(lstGioHang!=null)
            {
                iSoLuong = lstGioHang.Sum(n => n.iSoLuong);
                
            }
            return iSoLuong;
        }

        private Double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGioHang = Session["GioHang" + Session["DangNhap"]] as List<Giohang>;
            if (lstGioHang != null)
            {
                iTongTien = lstGioHang.Sum(n => n.dThanhTien) ;

            }
            return iTongTien;
        }

        public ActionResult ThongtinKH()
        {
            return View();
        }

        public ActionResult DatHang(Order order)
        {
            List<Giohang> lstGioHang1 = Session["GioHang" + Session["DangNhap"]] as List<Giohang>;
            List<Giohang> lstGioHang = Session["DonHangDaDat" + Session["DangNhap"]] as List<Giohang>;
            foreach(var item in lstGioHang1)
            {
                lstGioHang.Add(item); //lỗi
            }
            if (lstGioHang == null)
            {
                //Nếu giỏ hang chưa tồn tại thì ta khởi tạo mới list giỏ hàng
                lstGioHang = new List<Giohang>();
                Session["DonHangDaDat" + Session["DangNhap"]] = lstGioHang;
            }
            Session["GioHang" + Session["DangNhap"]] = null;
            //Thêm đơn hàng
            //Order order = new Order();
            order.Users_id =(int) Session["User_id"];
            order.ngaytao = DateTime.Now;
            order.tongtien = 100000;
            order.giaohang = "COD";
            db.Orders.Add(order);
            db.SaveChanges();
            List<Giohang> gh = LayGioHang();
            
            foreach(var item in gh)
            {
                Orders_Details ordersD = new Orders_Details();
                ordersD.Order_id = order.Order_id;
                ordersD.products_id = item.iMaSP;
                ordersD.soluongsp = item.iSoLuong;
                ordersD.tongtien = item.dThanhTien;
                db.Orders_Details.Add(ordersD);
                //XoaGioHang(item.iMaSP);
            }
            db.SaveChanges();
            return Redirect("Index");
        }

        public List<Giohang> LayDonHangDaDat()
        {
            List<Giohang> lstDonHangDaDat = Session["DonHangDaDat" + Session["DangNhap"]] as List<Giohang>; //ép kiểu session kiểu giỏ hàng
            if (lstDonHangDaDat == null)
            {
                //Nếu giỏ hang chưa tồn tại thì ta khởi tạo mới list giỏ hàng
                lstDonHangDaDat = new List<Giohang>();
                Session["DonHangDaDat" + Session["DangNhap"]] = lstDonHangDaDat;
            }
            return lstDonHangDaDat;
        }

        public ActionResult DonDatHang()
        {
            List<Giohang> lstGioHang = LayDonHangDaDat();
            return View(lstGioHang);
        }
    }
}
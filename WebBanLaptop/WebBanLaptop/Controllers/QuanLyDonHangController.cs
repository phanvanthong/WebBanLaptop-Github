using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.Script.Serialization;

namespace WebBanLaptop.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        // GET: QuanLyDonHang
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public ActionResult DonHangChuaXacNhan()
        {
            List<Order> lstOrder = db.Orders.Where(n => n.trangthai == "Chưa xác nhận").ToList();
            return View(lstOrder);
        }

        public ActionResult DonHangDangXuLy()
        {
            List<Order> lstOrder = db.Orders.Where(n => n.trangthai == "Đang xử lý").ToList();
            return View(lstOrder);
        }

        public ActionResult DonHangDaGiao()
        {
            List<Order> lstOrder = db.Orders.Where(n => n.trangthai == "Đã giao hàng").ToList();
            return View(lstOrder);
        }
        
        public ActionResult DonHangBiHuy()
        {
            List<Order> lstOrder = db.Orders.Where(n => n.trangthai == "Bị hủy").ToList();
            return View(lstOrder);
        }

        public ActionResult ChiTietDonHang(int id)
        {
            List<Orders_Details> lstOrderD = db.Orders_Details.Where(n => n.Order_id == id).ToList();
            Order order = db.Orders.SingleOrDefault(n => n.Order_id == id);
            ViewBag.user = order.User.fullname;
            ViewBag.orderID = order;
            return View(lstOrderD);
        }

        public ActionResult CapNhatTrangThai(Order order)
        {            
            if (Convert.ToInt32(order.trangthai) == 1)
            {
                order.trangthai = "Chưa xác nhận";
            }
            else if (Convert.ToInt32(order.trangthai) == 2)
            {
                order.trangthai = "Đang xử lý";
            }
            else if (Convert.ToInt32(order.trangthai) == 3)
            {
                order.trangthai = "Đã giao hàng";
            }
            else if (Convert.ToInt32(order.trangthai) == 4)
            {
                order.trangthai = "Bị hủy";
            }
            db.SaveChanges();
            return RedirectToAction("ChiTietDonHang", "QuanLyDonHang", new { @id = order.Order_id }); 
        }

        //#region Thay đổi trạng thái đơn hàng
        //public JsonResult TrangThaiDonHang(string cartModel,int trangthai)
        //{
        //   // int tt = parseInt(trangthai);
        //    var jsonCart = new JavaScriptSerializer().Deserialize<Order>(cartModel);
        //    Order order = db.Orders.SingleOrDefault(n => n.Order_id == jsonCart.Order_id);
        //    string trangthaiDH = "";
        //    if( trangthai == 1)
        //    {
        //        trangthaiDH = "Chưa xác nhận";
        //    }
        //    else if (trangthai == 2)
        //    {
        //        trangthaiDH = "Đang xử lý";
        //    }
        //    else if (trangthai == 3)
        //    {
        //        trangthaiDH = "Đã giao hàng";
        //    }
        //    else if (trangthai == 4)
        //    {
        //        trangthaiDH = "Bị hủy";
        //    }
        //    order.trangthai = trangthaiDH;
        //    db.SaveChanges();
        //    return Json(new
        //    {
        //        status = true
        //    });
        //}
        //#endregion
    }
}
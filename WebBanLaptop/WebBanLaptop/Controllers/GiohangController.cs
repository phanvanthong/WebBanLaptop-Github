using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebBanLaptop.Models;

namespace WebBanLaptop.Controllers
{
    public class GiohangController : Controller
    {
        // GET: Giohang
        //lấy giỏ hàng
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();
        public ActionResult Index()
        {

            List<Giohang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        public List<Giohang> LayGioHang()
        {
            if(Session["DangNhap"]!=null)
            {
                List<Giohang> lstGioHang = Session["GioHang" + Session["DangNhap"]] as List<Giohang>; //ép kiểu session kiểu giỏ hàng
                if (lstGioHang == null)
                {
                    lstGioHang = new List<Giohang>();
                    Session["GioHang" + Session["DangNhap"]] = lstGioHang;
                }

                if(Session["GioHang"]!=null)
                {
                    List<Giohang> lstgiohang1 = Session["GioHang"] as List<Giohang>;
                    foreach (var item in lstgiohang1)
                    {
                        lstGioHang.Add(item);
                    }
                    Session["GioHang"] = null;
                }
                return lstGioHang;
                
            }
            else
            {
                List<Giohang> lstgiohang1 = Session["GioHang"] as List<Giohang>;
                if (lstgiohang1 == null)
                {
                    lstgiohang1 = new List<Giohang>();
                    Session["GioHang"] = lstgiohang1;
                }
                return lstgiohang1;
            }

        }

        public ActionResult DatHangThanhCong()
        {
            return View();
        }

        //Thêm giỏ hang
        public ActionResult ThemGioHang(int iMaSP, string strURL)
        {
            Product product = db.Products.SingleOrDefault(n => n.Products_id == iMaSP);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<Giohang> lstGioHang = LayGioHang();
            Giohang gh = lstGioHang.Find(n => n.iMaSP == iMaSP);
            if (gh == null)
            {
                //gh = new Giohang(iMaSP);
                gh = new Giohang();
                Product prod = db.Products.SingleOrDefault(n => n.Products_id == product.Products_id);
                gh.iMaSP = prod.Products_id;
                gh.sHinhAnh = "img";
                gh.sTenSP = prod.Name;
                Discount discount = db.Discounts.SingleOrDefault(n => n.Discount_id == product.Discount_id);
                gh.dKhuyenMai = (Double)discount.value;
                gh.dDonGia = Convert.ToDouble(prod.Gia);
                gh.iSoLuong++;
                lstGioHang.Add(gh);
                return Redirect(strURL);
            }
            else
            {
                gh.iSoLuong++;
                return Redirect(strURL);
            }

        }

        //public ActionResult CapnhatGioHang(int iMaSP,FormCollection f)
        //{
        //    //Kiểm tra mã sp
        //    Product product = db.Products.SingleOrDefault(n=>n.Products_id==iMaSP);
        //    if(product==null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    List<Giohang> lstGioHang = LayGioHang();
        //    Giohang gh = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
        //    if(gh!=null)
        //    {
        //        gh.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
        //    }
        //    return View("GioHang");
        //}





        //private int TongSoLuong()
        //{
        //    int iSoLuong = 0;
        //    List<Giohang> lstGioHang = Session["GioHang" + Session["DangNhap"]] as List<Giohang>;
        //    if(lstGioHang!=null)
        //    {
        //        iSoLuong = lstGioHang.Sum(n => n.iSoLuong);

        //    }
        //    return iSoLuong;
        //}

        //private Double TongTien()
        //{
        //    double iTongTien = 0;
        //    List<Giohang> lstGioHang = Session["GioHang" + Session["DangNhap"]] as List<Giohang>;
        //    if (lstGioHang != null)
        //    {
        //        iTongTien = lstGioHang.Sum(n => n.dThanhTien) ;

        //    }
        //    return iTongTien;
        //}

        public ActionResult ThongtinKH()
        {
            if (Session["DangNhap"] == null)
            {
                return RedirectToAction("DangNhap", "Users");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThongtinKH(Order order, string city_id, string district_id, string ward_id, string sonha_id)
        {
            if (city_id == "")
            {
                ViewBag.Address = "Vui lòng chọn thành phố!";
                return View();
            }
            else if (district_id == "")
            {
                ViewBag.Address = "Vui lòng chọn quận huyện!";
                return View();
            }
            else if (ward_id == "")
            {
                ViewBag.Address = "Vui lòng chọn phường xã!";
                return View();
            }
            else if (sonha_id == "")
            {
                ViewBag.Address = "Vui lòng điền số nhà!";
                return View();
            }
            //return RedirectToAction("DatHang","Cart",  new { order, sonha_id, district_id, ward_id });

            List<Giohang> lstGioHang = Session["GioHang" + Session["DangNhap"]] as List<Giohang>;
            User user = Session["User"] as User;

            order.address += sonha_id;
            Dictionary<string, RootObject> items = new Dictionary<string, RootObject>();
            string tmp = "";
            var pathWard = Path.Combine(Server.MapPath("~/Content/NguoiDungCssLayout/json/xa-phuong/" + district_id + ".json"));
            using (StreamReader rd = new StreamReader(pathWard))
            {
                tmp = rd.ReadToEnd();
                items = JsonConvert.DeserializeObject<Dictionary<string, RootObject>>(tmp);
                tmp = "";
                foreach (var item in items)
                {
                    if (item.Key == ward_id)
                    {
                        order.address += ", " + item.Value.Path_with_type;
                        break;
                    }
                }
            }
            order.Users_id = user.Users_id;
            order.ngaytao = DateTime.Now;
            order.tongtien = (double)Session["TienThanhToan"];
            order.trangthai = "Chưa xác nhận";

            db.Orders.Add(order);
            db.SaveChanges();
            List<Giohang> gh = LayGioHang();

            foreach (var item in gh)
            {
                Orders_Details orderD = new Orders_Details();
                orderD.Order_id = order.Order_id;
                orderD.products_id = item.iMaSP;
                orderD.soluongsp = item.iSoLuong;
                orderD.tongtien = item.dThanhTien;
                db.Orders_Details.Add(orderD);
                //string[] arrListStr = str.Split(',');
            }
            db.SaveChanges();
            Session["GioHang" + Session["DangNhap"]] = null;
            return Redirect("DatHangThanhCong");
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
            if (gh != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSP == iMaSP);
            }
            return Redirect("Index");
        }

        public ActionResult DatHang(Order order)
        {
            //List<Giohang> lstgiohangnew = Session["GioHang"] as List<Giohang>;
            List<Giohang> lstGioHang1 = Session["GioHang" + Session["DangNhap"]] as List<Giohang>;
            //List<Giohang> lstGioHang = Session["DonHangDaDat" + Session["DangNhap"]] as List<Giohang>; 
            //if (lstGioHang == null)
            //{
            //    //Nếu giỏ hang chưa tồn tại thì ta khởi tạo mới list giỏ hàng
            //    lstGioHang = new List<Giohang>();
            //    Session["DonHangDaDat" + Session["DangNhap"]] = lstGioHang;
            //    foreach (var item in lstGioHang1)
            //    {
            //        lstGioHang.Add(item);
            //    }
            //}
            //else
            //{
            //    foreach (var item in lstGioHang1)
            //    {
            //        lstGioHang.Add(item);
            //    }
            //}
            
            //Session["GioHang" + Session["DangNhap"]] = null;
            //Thêm đơn hàng
            //Order order = new Order();
            User us = Session["User"] as User;

            order.Users_id = us.Users_id;
            order.ngaytao = DateTime.Now;
            order.tongtien = (double)Session["TienThanhToan"];
            //order.giaohang = "COD";
            db.Orders.Add(order);
            db.SaveChanges();
            List<Giohang> gh = LayGioHang();

            foreach (var item in gh)
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
            Session["GioHang" + Session["DangNhap"]] = null;
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
            if(Session["DangNhap"]==null)
            {
                return RedirectToAction("DangNhap", "Users");
            }
            else
            {
                string name = (string)Session["DangNhap"];
                User user = db.Users.Where(n => n.username == name).FirstOrDefault();
                Order order = db.Orders.Where(n => n.Users_id == user.Users_id).FirstOrDefault();
                List<Order> lstOrder = db.Orders.Where(n => n.Users_id == user.Users_id).ToList();
                var orderDetail = db.Orders_Details.OrderBy(n=>n.Order_id).ToList();
                ViewBag.Detail = orderDetail;
                return View(lstOrder);
            }
            
        }

        //Thêm sản phẩm vào giỏ hàng
        #region Thêm vào giỏ hàng
        public JsonResult AddItem(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<Giohang>(cartModel);
            Product product = db.Products.SingleOrDefault(n => n.Products_id == jsonCart.iMaSP);
            List<Giohang> Cart = LayGioHang();
            if(Cart != null)
            {
                
                if(Cart.Exists(n=>n.iMaSP == product.Products_id))
                {
                    foreach (var item in Cart)
                    {
                        if (item.iMaSP == product.Products_id)
                            item.iSoLuong++;
                    }
                }
                else
                {
                    Giohang gh = new Giohang(); 
                   //gh.iMaSP = product.Products_id;
                    Product prod = db.Products.SingleOrDefault(n => n.Products_id== product.Products_id);
                    gh.iMaSP = prod.Products_id;
                    gh.sHinhAnh = "img";
                    gh.sTenSP = prod.Name;
                    Discount discount = db.Discounts.SingleOrDefault(n => n.Discount_id == product.Discount_id);
                    gh.dKhuyenMai = (Double)discount.value;
                    gh.dDonGia = Convert.ToDouble( prod.Gia); 
                    gh.iSoLuong++; 
                    Cart.Add(gh);
                }

            }
            return Json(new
            {
                status = true 
            });
        }
        #endregion
        
        #region MuaNgay
        public ActionResult MuaNgay( int id)
        {
            //Product product = db.Products.SingleOrDefault(n => n.Products_id == id);
            //if (product == null)
            //{
            //    Response.StatusCode = 404;
            //    return null;
            //}
            //Lấy ra session giỏ hàng
            List<Giohang> lstGioHang = LayGioHang();
            Giohang gh = lstGioHang.Find(n => n.iMaSP == id);
            if (gh == null)
            {
                gh = new Giohang();
                //gh.iMaSP = product.Products_id;
                Product prod = db.Products.SingleOrDefault(n => n.Products_id == id);
                gh.iMaSP = prod.Products_id;
                gh.sHinhAnh = "img";
                gh.sTenSP = prod.Name;
                Discount discount = db.Discounts.SingleOrDefault(n => n.Discount_id == prod.Discount_id);
                gh.dKhuyenMai = (Double)discount.value;
                gh.dDonGia = Convert.ToDouble(prod.Gia);
                gh.iSoLuong++;
                lstGioHang.Add(gh);
                return RedirectToAction("Index");
            }
            else
            {
                gh.iSoLuong++;
                return RedirectToAction("Index");
            }

        }
        #endregion

        #region MuaNgayTrongChiTiet

        public JsonResult MuaNgayChiTiet(string cartModel)
        {

            var jsonCart = new JavaScriptSerializer().Deserialize<Giohang>(cartModel);
            Product product = db.Products.SingleOrDefault(n => n.Products_id == jsonCart.iMaSP);
            List<Giohang> Cart = LayGioHang();
            if (Cart != null)
            {

                if (Cart.Exists(n => n.iMaSP == product.Products_id))
                {
                    foreach (var item in Cart)
                    {
                        if (item.iMaSP == product.Products_id)
                            item.iSoLuong += jsonCart.iSoLuong;
                    }
                }
                else
                {
                    Giohang gh = new Giohang();
                    //gh.iMaSP = product.Products_id;
                    Product prod = db.Products.SingleOrDefault(n => n.Products_id == product.Products_id);
                    gh.iMaSP = prod.Products_id;
                    gh.sHinhAnh = "img";
                    gh.sTenSP = prod.Name;
                    Discount discount = db.Discounts.SingleOrDefault(n => n.Discount_id == product.Discount_id);
                    gh.dKhuyenMai = (Double)discount.value;
                    gh.dDonGia = Convert.ToDouble(prod.Gia);
                    gh.iSoLuong = jsonCart.iSoLuong;
                    Cart.Add(gh);
                }

            }

            return Json(new
            {
                status = true
                
            });
        }
        #endregion

        #region Thêm vào giỏ hàng trong chi tiết
        public JsonResult AddItemChiTiet(string cartModel)
        {
            
            var jsonCart = new JavaScriptSerializer().Deserialize<Giohang>(cartModel);
            Product product = db.Products.SingleOrDefault(n => n.Products_id == jsonCart.iMaSP);
            List<Giohang> Cart = LayGioHang();
            if (Cart != null)
            {

                if (Cart.Exists(n => n.iMaSP == product.Products_id))
                {
                    foreach (var item in Cart)
                    {
                        if (item.iMaSP == product.Products_id)
                            item.iSoLuong+=jsonCart.iSoLuong;
                    }
                }
                else
                {
                    Giohang gh = new Giohang();
                    //gh.iMaSP = product.Products_id;
                    Product prod = db.Products.SingleOrDefault(n => n.Products_id == product.Products_id);
                    gh.iMaSP = prod.Products_id;
                    gh.sHinhAnh = "img";
                    gh.sTenSP = prod.Name;
                    Discount discount = db.Discounts.SingleOrDefault(n => n.Discount_id == product.Discount_id);
                    gh.dKhuyenMai = (Double)discount.value;
                    gh.dDonGia = Convert.ToDouble(prod.Gia);
                    gh.iSoLuong = jsonCart.iSoLuong;
                    Cart.Add(gh);
                }

            }
            return Json(new
            {
                status = true
            });
        }
        #endregion

        #region Chỉnh sửa số lượng trong giỏ hàng
        public JsonResult AddItemGioHang(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<Giohang>(cartModel);
            //Product product = db.Products.SingleOrDefault(n => n.Products_id == jsonCart.iMaSP);
            List <Giohang> Cart = LayGioHang();
            if (Session["DangNhap"] != null)
            {
                List<Giohang> lstGioHang = Session["GioHang" + Session["DangNhap"]] as List<Giohang>; //ép kiểu session kiểu giỏ hàng
                Giohang gh = lstGioHang.Find(n => n.iMaSP == jsonCart.iMaSP);
                gh.iSoLuong = jsonCart.iSoLuong;
            }
            else
            {
                List<Giohang> lstGioHang = Session["GioHang"] as List<Giohang>; //ép kiểu session kiểu giỏ hàng
                Giohang gh = lstGioHang.Find(n => n.iMaSP == jsonCart.iMaSP);
                gh.iSoLuong = jsonCart.iSoLuong;
            }
            return Json(new
            {
                status = true
            });
        }
        #endregion

        #region Load dữ liệu quận huyện
        public JsonResult LoadData_District(string city_ID)
        {
            string tmp = "";
            if (city_ID != "")
            {
                var pathJson = Path.Combine(Server.MapPath("~/Content/NguoiDungCssLayout/json/quan-huyen/" + city_ID + ".json"));
                //ObJson json = new ObJson();
                using (StreamReader r = new StreamReader(pathJson))
                {
                    tmp = r.ReadToEnd();
                    r.Close();
                }
            }


            return Json(new
            {
                status = true,
                data = tmp
            });
        }
        #endregion

        #region Load dữ liệu Phường xã
        public JsonResult LoadData_Ward(string district_id)
        {
            string tmp = "";
            if (district_id != "")
            {
                var pathJson = Path.Combine(Server.MapPath("~/Content/NguoiDungCssLayout/json/xa-phuong/" + district_id + ".json"));
                //ObJson json = new ObJson();            
                using (StreamReader r = new StreamReader(pathJson))
                {
                    tmp = r.ReadToEnd();
                    r.Close();
                }
            }


            return Json(new
            {
                status = true,
                data = tmp
            });
        }
        #endregion

    }
}
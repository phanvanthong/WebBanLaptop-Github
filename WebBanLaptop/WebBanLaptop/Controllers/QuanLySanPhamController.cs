using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace WebBanLaptop.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        Web_ban_laptopEntities db = new Web_ban_laptopEntities();

        
        //}

        

        //[HttpGet]

        //public ActionResult TimKiemLaptop(string TuKhoaLaptop, int page=1)
        //{            
        //    ViewBag.TuKhoaLaptop = TuKhoaLaptop;
        //    string query = "select *from products where name like '%" + TuKhoaLaptop + "%'";
        //    List<Product> lstKQTK = db.Database.SqlQuery<Product>(query).ToList<Product>();
        //    if (lstKQTK.Count == 0)
        //    {
        //        ViewBag.TimKiemLaptop = "Không tìm thấy kết quả nào!";
        //    }
        //    else
        //    {
        //        ViewBag.TimKiemLaptop = "Tìm thấy " + lstKQTK.Count + " kết quả!";
        //    }
        //    int pageNumber = page;
        //    int pageSize = 12;
        //    //ViewBag.SlLaptop = lstKQTK.Count();
        //    return View(db.Database.SqlQuery<Product>(query).ToList<Product>().ToPagedList(pageNumber, pageSize));         
                        
        //}

        public ActionResult TimKiemLaptop(string TuKhoaLaptop, string hangsx, string gia, string manhinh, string khuyenmai, string RAM, string ocung, int page = 1)
        {
            ViewBag.TuKhoaLaptop = TuKhoaLaptop;
            ViewBag.hangsx = hangsx;
            ViewBag.gia = gia;
            ViewBag.manhinh = manhinh;
            ViewBag.khuyenmai = khuyenmai;
            ViewBag.RAM = RAM;
            ViewBag.ocung = ocung;
            string query = "exec DkLaptop '" + TuKhoaLaptop + "','" + hangsx + "','" + gia + "','" + manhinh + "','" + khuyenmai + "','" + RAM + "','" + ocung + "'";
            List<Product> lstKQTK = db.Database.SqlQuery<Product>(query).ToList<Product>();
            if (lstKQTK.Count == 0)
            {
                ViewBag.TimKiemLaptop = "Không tìm thấy kết quả nào!";
            }
            else
            {
                ViewBag.TimKiemLaptop = "Tìm thấy " + lstKQTK.Count + " kết quả!";
            }
            int pageNumber = page;
            int pageSize = 12;
            return View(db.Database.SqlQuery<Product>(query).ToList<Product>().ToPagedList(pageNumber, pageSize));
            
        }


        public ActionResult TimKiemKM(string TuKhoaKM, int page = 1)
        {
            ViewBag.TuKhoaKM = TuKhoaKM;
            string query = "select *from Discount where value like '%" + TuKhoaKM + "%'";
            List<Discount> lstKQTK = db.Database.SqlQuery<Discount>(query).ToList<Discount>();
            if (lstKQTK.Count == 0)
            {
                ViewBag.TimKiemKM = "Không tìm thấy kết quả nào!";
            }
            else
            {
                ViewBag.TimKiemKM = "Tìm thấy " + lstKQTK.Count + " kết quả!";
            }
            int pageNumber = page;
            int pageSize = 12;
            return View(db.Database.SqlQuery<Discount>(query).ToList<Discount>().ToPagedList(pageNumber, pageSize));

        }

        public ActionResult TimKiemHangsx(string TuKhoaHangsx, int page = 1)
        {
            ViewBag.TuKhoaHangsx = TuKhoaHangsx;
            string query = "select *from Hangsx where tenhang like '%" + TuKhoaHangsx + "%'";
            List<Hangsx> lstKQTK = db.Database.SqlQuery<Hangsx>(query).ToList<Hangsx>();
            if (lstKQTK.Count == 0)
            {
                ViewBag.TimKiemHangsx = "Không tìm thấy kết quả nào!";
            }
            else
            {
                ViewBag.TimKiemHangsx = "Tìm thấy " + lstKQTK.Count + " kết quả!";
            }
            int pageNumber = page;
            int pageSize = 12;
            return View(lstKQTK.ToPagedList(pageNumber, pageSize));

        }

        //Quản lý Laptop
        public ActionResult ListLaptop(int? page) //List laptop
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            return View(db.Products.ToList().OrderBy(n=>n.Products_id).ToPagedList(pageNumber,pageSize));
        }

        //----------

        [HttpGet]
        public ActionResult ThemMoiLaptop()
        {
            
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            //ViewBag.Discount_id = new SelectList(db.Discounts.ToList().OrderBy(n => n.Discount_id), "Discount_id", "Value");
            //ViewBag.Hangsx_id = new SelectList(db.Hangsxes.ToList().OrderBy(n => n.Hangsx_id), "Hangsx_id", "tenhang");
            ViewBag.HangSX = db.Hangsxes.OrderBy(n => n.tenhang).ToList();
            ViewBag.Discount = db.Discounts.OrderBy(n => n.value).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiLaptop(Product product, HttpPostedFileBase fileupload)
        {
            string directoryPath = "E:/Ki_2_nam_3/Cong nghe web/MVC/WebBanLaptop-Github/WebBanLaptop/WebBanLaptop/Content/Images/i3/" + product.Products_id;
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }
            var fileName = Path.GetFileName(fileupload.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/Images/i3/" + product.Products_id), fileName);//đây là gán tên vào đường dẫn
            
            if (System.IO.File.Exists(path))
            {
                ViewBag.ThongBao = "Hình ảnh đã tồn tại";
            }
            else
            {
                fileupload.SaveAs(path);
            }
            db.Products.Add(product);
            db.SaveChanges();
            return Redirect("ListLaptop"); 
        }

        //---------- 

        public ActionResult ChinhSuaLaptop(int id=0)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }             
            Product product = db.Products.SingleOrDefault(n => n.Products_id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.HangSX = db.Hangsxes.OrderBy(n => n.tenhang).ToList();
            ViewBag.Discount = db.Discounts.OrderBy(n => n.value).ToList();
            return View(product);
        }
        [HttpPost, ActionName("ChinhSuaLaptop")]
        public ActionResult XacNhanChinhSuaLaptop(Product product)
        {
            //product.Hangsx_id = Hangsx_id;
            //product.Discount_id = Discount_id; 
            //if (ModelState.IsValid)
            //{
                db.Entry(product).State = System.Data.Entity.EntityState.Modified; 
                db.SaveChanges();
            //}
            return RedirectToAction("ListLaptop");
        }

        //----------

        public ActionResult XoaLaptop(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            Product product = db.Products.SingleOrDefault(n => n.Products_id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }
        
        [HttpPost,ActionName("XoaLaptop")]
        public ActionResult XacNhanXoaLaptop(int id)
        {
            Product product = db.Products.SingleOrDefault(n => n.Products_id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ListLaptop");
        }

        //----------

        public ActionResult HienThiLaptop(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            Product product = db.Products.SingleOrDefault(n => n.Products_id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }


        //--------------------------------------------------------------
        //Quản Lý hãng sản xuất
        public ActionResult ListHangsx(int? page) //List Hangsx
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            return View(db.Hangsxes.ToList().OrderBy(n => n.Hangsx_id).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemMoiHangsx()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiHangsx(Hangsx hangsx)
        {
            db.Hangsxes.Add(hangsx);
            db.SaveChanges();
            return RedirectToAction("ListHangsx");
        }

        //----------

        public ActionResult ChinhSuaHangsx(int id = 0)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            Hangsx hangsx = db.Hangsxes.SingleOrDefault(n => n.Hangsx_id == id);
            if (hangsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hangsx);
        }
        [HttpPost, ActionName("ChinhSuaHangsx")]
        public ActionResult XacNhanChinhSuaHangsx(Hangsx hangsx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangsx).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ListHangsx");
        }

        //----------

        public ActionResult XoaHangsx(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            Hangsx hangsx = db.Hangsxes.SingleOrDefault(n => n.Hangsx_id == id);
            if (hangsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hangsx);
        }

        [HttpPost, ActionName("XoaHangsx")]
        public ActionResult XacNhanXoaHangsx(int id)
        {
            Hangsx hangsx = db.Hangsxes.SingleOrDefault(n => n.Hangsx_id == id);
            if (hangsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Hangsxes.Remove(hangsx);
            db.SaveChanges();
            return RedirectToAction("ListHangsx");
        }

        //----------

        public ActionResult HienThiHangsx(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            Hangsx hangsx = db.Hangsxes.SingleOrDefault(n => n.Hangsx_id == id);
            if (hangsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hangsx);
        }

        //--------------------------------------------------------------
        //Quản Lý Khuyến mãi
        public ActionResult ListKM(int? page) //List Khuyến mại
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            return View(db.Discounts.ToList().OrderBy(n => n.Discount_id).ToPagedList(pageNumber, pageSize));
        }

        //----------

        [HttpGet]
        public ActionResult ThemMoiKM()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiKM(Discount discount)
        {
            db.Discounts.Add(discount);
            db.SaveChanges();
            return RedirectToAction("ListKM");
        }

        //----------

        public ActionResult ChinhSuaKM(int id = 0)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            Discount discount = db.Discounts.SingleOrDefault(n => n.Discount_id == id);
            if (discount == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(discount);
        }
        [HttpPost, ActionName("ChinhSuaKM")]
        public ActionResult XacNhanChinhSuaKM(Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ListKM");
        }

        //----------

        public ActionResult XoaKM(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            Discount discount = db.Discounts.SingleOrDefault(n => n.Discount_id == id);
            if (discount == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(discount);
        }

        [HttpPost, ActionName("XoaKM")]
        public ActionResult XacNhanXoaHKM(int id)
        {
            Discount discount = db.Discounts.SingleOrDefault(n => n.Discount_id == id);
            if (discount == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Discounts.Remove(discount);
            db.SaveChanges();
            return RedirectToAction("ListKM");
        }

        //----------

        public ActionResult HienThiKM(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            Discount discount = db.Discounts.SingleOrDefault(n => n.Discount_id == id);
            if (discount == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(discount);
        }

    
        //--------------------------------------------------------------

        //public JsonResult LoadIMG(string filePath)
        //{
        //    string[] str = new string[10];
        //}


    }
}
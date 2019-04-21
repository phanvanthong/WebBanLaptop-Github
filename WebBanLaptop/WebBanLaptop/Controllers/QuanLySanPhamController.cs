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

        //public ActionResult Index()
        //{
        //    if (Session["DangNhapAdmin"] == null)
        //    {
        //        return RedirectToAction("DangNhap", "Admin");
        //    }
        //    return View();
        //}


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
            ViewBag.Discount_id = new SelectList(db.Discounts.ToList().OrderBy(n => n.Discount_id), "Discount_id", "Value");
            ViewBag.Hangsx_id = new SelectList(db.Hangsxes.ToList().OrderBy(n => n.Hangsx_id), "Hangsx_id", "tenhang");
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiLaptop(Product product, HttpPostedFileBase fileupload)
        {
            //string directoryPath = "~/Content/Images/i3/" + product.Products_id;
            //if (!System.IO.Directory.Exists(directoryPath))
            //{
            //    System.IO.Directory.CreateDirectory(directoryPath);
            //}
            ////lưu tên file
            //var fileName = Path.GetFileName(fileupload.FileName);
            ////lưi đường dẫn của file 
            //var path = Path.Combine(Server.MapPath("~/Content/Images/i3/" + product.Products_id), fileName);
            ////Kiểm tra ảnh đã tồn tại chưa
            //if (System.IO.File.Exists(path))
            //{
            //    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
            //}
            //else
            //{
            //    fileupload.SaveAs(path);
            //}

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
            return View(product);
        }
        [HttpPost, ActionName("ChinhSuaLaptop")]
        public ActionResult XacNhanChinhSuaLaptop(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

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

        //Quản Lý Ram
        //public ActionResult ListRam(int? page) //List Ram
        //{
        //    int pageNumber = (page ?? 1);
        //    int pageSize = 12;
        //    return View(db.Products.ToList().OrderBy(n => n.Products_id).ToPagedList(pageNumber, pageSize));
        //}

        //[HttpGet]
        //public ActionResult ThemMoiRam()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult ThemMoiRam(Product product)
        //{
        //    db.Products.Add(product);
        //    db.SaveChanges();
        //    RedirectToAction("Index");
        //    return View();
        //}

        ////----------

        //public ActionResult ChinhSuaRam(int id = 0)
        //{
        //    Product product = db.Products.SingleOrDefault(n => n.Products_id == id);
        //    if (product == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    return View(product);
        //}
        //[HttpPost, ActionName("ChinhSua")]
        //public ActionResult XacNhanChinhSuaRam(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}

        ////----------

        //public ActionResult XoaRam(int id)
        //{
        //    Product product = db.Products.SingleOrDefault(n => n.Products_id == id);
        //    if (product == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    return View(product);
        //}

        //[HttpPost, ActionName("Xoa")]
        //public ActionResult XacNhanXoaRam(int id)
        //{
        //    Product product = db.Products.SingleOrDefault(n => n.Products_id == id);
        //    if (product == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    db.Products.Remove(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        ////----------

        //public ActionResult HienThiRam(int id)
        //{
        //    Product product = db.Products.SingleOrDefault(n => n.Products_id == id);
        //    if (product == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    return View(product);
        //}

        //--------------------------------------------------------------
        //Quản Lý Ổ đĩa




    }
}
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
            // có rồi còn gì, có dropdown r cơ mà hiện ra cái phù hợp vs ý là lúc sửa muốn hiện ở dropdown là tên hãng của đúng sản phẩm dấy luôn ấy hả ừm
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
           //cái này là tạo đường dẫn để tạo folder
            string directoryPath = "E:/Ki_2_nam_3/Cong nghe web/MVC/WebBanLaptop-Github/WebBanLaptop/WebBanLaptop/Content/Images/i3/" + product.Products_id;
            if (!System.IO.Directory.Exists(directoryPath))//ktra đã tồn tại folder chưa để khởi tạo
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }
            //lưu tên file file javascrip của ô để đâu
            var fileName = Path.GetFileName(fileupload.FileName);//đây là lấy tên
            //lưi đường dẫn của file // thấy chưa thực chất nó chỉ lấy tên file thôi
            var path = Path.Combine(Server.MapPath("~/Content/Images/i3/" + product.Products_id), fileName);//đây là gán tên vào đường dẫn
            //Kiểm tra ảnh đã tồn tại chưa
            if (System.IO.File.Exists(path))
            {
                ViewBag.ThongBao = "Hình ảnh đã tồn tại";
            }
            else
            {
                fileupload.SaveAs(path);//cái chỗ nó lưu đây cơ mà @@ thì kothaays à thực chất nó chỉ luuw một dường dẫn ảnh chứ cỏ phải nó copy một cảnh sang đâu
                // nó lấy tên ảnh, tạo một chuỗi đường dẫn ảnh vào server: Con
            }
            db.Products.Add(product);
            db.SaveChanges();
            return Redirect("ListLaptop"); 
        }

        //---------- cứ để ko dùng dropdowwn lits

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
            //product.Discount_id = Discount_id; // để t debug lại tử từ cứ tranh @@// lỗi vì ko có ảnh//k cần ảnh trong product k có ảnh
            //if (ModelState.IsValid)//false này đây nhé 2 cái kia null ko liên qua vì nó ko phải là thuộc tính cảu product để coi lại sao lỗi
            //{
                db.Entry(product).State = System.Data.Entity.EntityState.Modified; // này không phải kiểm tra, này là gán tất cả thay đổi trong csdl à quên :)))
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
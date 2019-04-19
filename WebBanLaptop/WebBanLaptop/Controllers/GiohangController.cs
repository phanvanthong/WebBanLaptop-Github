 using System;
using System.Collections.Generic;
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
        //lấy giò hàng
        Web_ban_laptopEntities db = new Web_ban_laptopEntities(); // cứ 03n y3n 0i @@
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
        //public ActionResult ThemGioHang(int iMaSP,string strURL )
        //{
        //    if (Session["DangNhap"] == null)
        //    {
        //        return RedirectToAction("DangNhap", "Users");
        //    }
        //    Product product = db.Products.SingleOrDefault(n => n.Products_id == iMaSP);
        //    if(product==null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    //Lấy ra session giỏ hàng
        //    List<Giohang> lstGioHang = LayGioHang();
        //    Giohang gh = lstGioHang.Find(n => n.iMaSP == iMaSP);
        //    if(gh==null)
        //    {
        //        gh = new Giohang(iMaSP);
        //        lstGioHang.Add(gh);
        //        return Redirect(strURL);
        //    }
        //    else
        //    {
        //        gh.iSoLuong ++;
        //        return Redirect(strURL);
        //    }

        //}

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

        ////Xóa giỏ hàng
        //public ActionResult XoaGioHang(int iMaSP)
        //{
        //    //kiểm tra mã sp
        //    Product product = db.Products.SingleOrDefault(n => n.Products_id == iMaSP);
        //    if (product == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    //lấy giỏ hàng từ session
        //    List<Giohang> lstGioHang = LayGioHang();
        //    Giohang gh = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
        //    //Nếu  mà tồn tại thì ta sửa số lượng
        //    if(gh!=null)
        //    {
        //        lstGioHang.RemoveAll(n => n.iMaSP == iMaSP);
        //    }
        //    return Redirect("Index");
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

        //public ActionResult ThongtinKH()
        //{
        //    return View();
        //}

        //public ActionResult DatHang(Order order)
        //{
        //    //List<Giohang> lstgiohangnew = Session["GioHang"] as List<Giohang>;
        //    List<Giohang> lstGioHang1 = Session["GioHang" + Session["DangNhap"]] as List<Giohang>;
        //    List<Giohang> lstGioHang = Session["DonHangDaDat" + Session["DangNhap"]] as List<Giohang>;
        //    foreach(var item in lstGioHang1)
        //    {
        //        lstGioHang.Add(item); //lỗi
        //    }
        //    if (lstGioHang == null)
        //    {
        //        //Nếu giỏ hang chưa tồn tại thì ta khởi tạo mới list giỏ hàng
        //        lstGioHang = new List<Giohang>();
        //        Session["DonHangDaDat" + Session["DangNhap"]] = lstGioHang;
        //    }
        //    Session["GioHang" + Session["DangNhap"]] = null;
        //    //Thêm đơn hàng
        //    //Order order = new Order();
        //    User us = Session["User"] as User;
        //    order.Users_id =us.Users_id;
        //    order.ngaytao = DateTime.Now;
        //    order.tongtien = 100000;
        //    order.giaohang = "COD";
        //    db.Orders.Add(order);
        //    db.SaveChanges();
        //    List<Giohang> gh = LayGioHang();

        //    foreach(var item in gh)
        //    {
        //        Orders_Details ordersD = new Orders_Details();
        //        ordersD.Order_id = order.Order_id;
        //        ordersD.products_id = item.iMaSP;
        //        ordersD.soluongsp = item.iSoLuong;
        //        ordersD.tongtien = item.dThanhTien;
        //        db.Orders_Details.Add(ordersD);
        //        //XoaGioHang(item.iMaSP);
        //    }
        //    db.SaveChanges();
        //    return Redirect("Index");
        //}

        //public List<Giohang> LayDonHangDaDat()
        //{
        //    List<Giohang> lstDonHangDaDat = Session["DonHangDaDat" + Session["DangNhap"]] as List<Giohang>; //ép kiểu session kiểu giỏ hàng
        //    if (lstDonHangDaDat == null)
        //    {
        //        //Nếu giỏ hang chưa tồn tại thì ta khởi tạo mới list giỏ hàng
        //        lstDonHangDaDat = new List<Giohang>();
        //        Session["DonHangDaDat" + Session["DangNhap"]] = lstDonHangDaDat;
        //    }
        //    return lstDonHangDaDat;
        //}

        //public ActionResult DonDatHang()
        //{
        //    List<Giohang> lstGioHang = LayDonHangDaDat();
        //    return View(lstGioHang);
        //}

        public ActionResult Index()
        {
            if (Session["DangNhap"] == null)
            {
                return RedirectToAction("DangNhap", "Users");
            }
            List<Giohang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        public JsonResult AddItem(string cartModel)
        {
            //
            //ngon r :V làm thêm cái thông báo nữa uk cũng đag tìm để thêm đây hiểu cái ajaax kia chưa chưa r0x ;là sao chưa rõ lắm// để mai goiuj Mess giờ đi ngủ uk ngủ đê
            // đây để t thử cách này/ ook rooid ô sửa j r >? gọi sự kiện click của class, nhưng vì có this.dataid nên nó hiểu đấy là của thẻ a nào luôn để thuwwe xem
            //vẫn phải để id chứ đê ở dưới lấy truyề vào kiểu gì để thế à ok ở đấy ô để class đc r còn id là hàm dưới gọi sửa thữ xem t không hiểu định làm như nào
            //Biết vì sao lõi rồi, vì cai thẻ a của ô đáy nó nhiều thẻ méo ổn rồi, không ô thêm 2 class vào là oke 1 id theo id cua sản phẩm
            // lỗi gì thế nà, ko vào 0c Giở hàng// trùng t3n dkm à rsao lỗi chính tả @@ dến View đi // ok rồi đáy đâu rồi đây thấy vì sao cái lớp Cart của t có đối tượng product chưa à r
            // Hình như do truyền vào là chuỗi mà bên kia của ô là một số để t tìm hiểu cái này, chwof tí/ ừm 0ể coi
            // Đây này vì nó từ cái id kia nó ko hiểu đc hàm khởi tạo một Giỏ hàng mới lỗi// wtf sao lịa gọi 0ến hàm này nhỉ đang k hiểu  :v
            //Đợi t debug lại cái của t xem sao nó laikj lỗi/ đâu rồi ê Thông ddaay sao r à timg thấy rồi chờ tí đã oke :))
            var jsonCart = new JavaScriptSerializer().Deserialize<Giohang>(cartModel);
            //hàm này lzi đấy sao vào giỏ hàng cung gọi, chuyển đổi dữ liệu Json sang text/ ok
            //cái addItem í sao hàm này lzi đấy sao vào giỏ hàng cung , gọi mỗi chõ ajax thôi mà ô debug nó gọi mafthif đan để  t4
            //sao đăng nhapapj cùng gọi cái nay thế, 0ể ciu
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
                    Giohang gh = new Giohang(); //Tại sao thêm mới một giỏ hàng pahir cso id đâ //lỗi 0ể xem lại
                   //gh.iMaSP = product.Products_id;
                    Product prod = db.Products.SingleOrDefault(n => n.Products_id== product.Products_id);
                    gh.iMaSP = prod.Products_id;
                    gh.sHinhAnh = "img";
                    gh.sTenSP = prod.Name;
                    gh.dDonGia = Convert.ToDouble( prod.Gia); // Hỏi ép ku
                    gh.iSoLuong++; // sửa thì chưa tính :)), đân nói ví dụ ô có một sản phẩm trong sesion rồi, sau quay lại mua tiếp làm sao nó cập nahatj đc session
                    //lúc hàm thêm ô gọi lại laygiohang thì trong hàm thêm có tồn tại session r nên trong hàm thêm ô chỉ cần add vào list thì session sẽ có àok
                    Cart.Add(gh);// nấy ấy hả uk đá
                }

            }
            return Json(new
            {
                status = true 
            });
        }
    }
}
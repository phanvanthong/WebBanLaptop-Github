using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebBanLaptop.Models
{
    [MetadataTypeAttribute(typeof(ProductMetada))]
    public partial class Product
    {
        internal sealed class ProductMetada
        {
            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Mã laptop")]
            public int Products_id { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Tên laptop")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Hãng CPU")]
            public string HangCPU { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Công nghệ laptop")]
            public string CNCPU { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Tốc độ max")]
            public string TocDoMax { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Ram")]
            public string MemoryRam { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Loại ram")]
            public string LoaiRam { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Bus ram")]
            public string BusRam { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Số khe ram")]
            public Nullable<int> KheRam { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Ổ đĩa")]
            public string ODia { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Dung lượng ổ đĩa")]
            public string DungLuongODia { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Card đồ họa")]
            public string CardDohoa { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Màn hình")]
            public string ManhHinh { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Độ phân giải")]
            public string Dophangiai { get; set; }
            

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Giá")]
            public Nullable<double> Gia { get; set; }
            
            [Display(Name = "Giá sau KM")]
            public Nullable<double> GiaKM { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Số lượng")]
            public Nullable<int> Soluong { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Khuyến mại")]
            public Nullable<int> Discount_id { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Hãng sản xuất")]
            public Nullable<int> Hangsx_id { get; set; }


        }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace WebBanLaptop.Models
{
    [MetadataTypeAttribute(typeof(DiscountMetadata))]
    public partial class Discount
    {
        internal sealed class DiscountMetadata
        {
            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "ID")]
            public int Discount_id { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Giá trị")]
            public double value { get; set; }


        }
    }
}
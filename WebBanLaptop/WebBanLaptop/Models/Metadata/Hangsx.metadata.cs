using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace WebBanLaptop.Models
{
    [MetadataTypeAttribute(typeof(HangsxMetadata))]
    public partial class Hangsx
    {
        internal sealed class HangsxMetadata
        {
            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "ID")]
            public int Hangsx_id { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại dữ liệu cho bảng này.")]
            [Display(Name = "Tên hãng")]
            public double tenhang { get; set; }
        }
    }
}
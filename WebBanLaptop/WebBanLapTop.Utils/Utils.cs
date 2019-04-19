using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLapTop.Utils
{
    public class Utils
    {
        public static string commaSeparateNumber(string val)
        {
            while (/ (\d +)(\d{ 3})/.test(val.toString())) {
                val = val.toString().replace(/ (\d +)(\d{ 3})/, '$1' + ',' + '$2');
            }
            return val;
        }
    }
}
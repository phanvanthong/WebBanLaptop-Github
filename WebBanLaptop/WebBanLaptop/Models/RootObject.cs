using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLaptop.Models
{
    public class RootObject
    {
        private string key;
        private string name;
        private string type;
        private string slug;
        private string name_with_type;
        private string path;
        private string path_with_type;
        private string code;
        private string parent_code;

        public RootObject(string key, string name, string type, string slug, string name_with_type, string path, string path_with_type, string code, string parent_code)
        {
            this.Key = key;
            this.Name = name;
            this.Type = type;
            this.Slug = slug;
            this.Name_with_type = name_with_type;
            this.Path = path;
            this.Path_with_type = path_with_type;
            this.Code = code;
            this.Parent_code = parent_code;
        }

        public string Key { get => key; set => key = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public string Slug { get => slug; set => slug = value; }
        public string Name_with_type { get => name_with_type; set => name_with_type = value; }
        public string Path { get => path; set => path = value; }
        public string Path_with_type { get => path_with_type; set => path_with_type = value; }
        public string Code { get => code; set => code = value; }
        public string Parent_code { get => parent_code; set => parent_code = value; }
    }
}
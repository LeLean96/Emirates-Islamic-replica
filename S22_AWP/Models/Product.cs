using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S22_AWP.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Image { get; set; }
        public string ImagePath { get { return UploadUtils.UploadedFilePath(Image); } }
    }
}

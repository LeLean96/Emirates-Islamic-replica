using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using S22_AWP.Data;
using S22_AWP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace S22_AWP.ViewModels
{
    public class ProductViewModel
    {
        public IFormFile Image { get; set; }
        public int ID { get; set; }
        public string ExistingImage { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string LongDescription { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public Product ToDB(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            Product dbProduct;
            if (ID != 0)
            {
                dbProduct = dbContext.Products.FirstOrDefault(m => m.ID == ID);
            }
            else
            {
                dbProduct = new Product();
            }
            string uniqueFileName = UploadUtils.ProcessUploadedFile(Image, webHostEnvironment);
            dbProduct.ID = ID;
            dbProduct.LongDescription = LongDescription;
            dbProduct.ShortDescription = ShortDescription;
            dbProduct.Title = Title;
            if (uniqueFileName != null)
            {
                dbProduct.Image = uniqueFileName;
            }
            return dbProduct;
        }
        public static ProductViewModel FromDB(Product Product)
        {
            return new ProductViewModel
            {
                ID = Product.ID,
                LongDescription = Product.LongDescription,
                ShortDescription = Product.ShortDescription,
                Title = Product.Title,
                ExistingImage = Product.Image
            };
        }
        public string ExistingImagePath { get { return UploadUtils.UploadedFilePath(ExistingImage); } }
    }
}

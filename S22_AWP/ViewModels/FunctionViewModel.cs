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
    public class FunctionViewModel
    {
        public IFormFile Image { get; set; }
        public int ID { get; set; }
        public string ExistingImage { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string LongDescription { get; set; }
        public Function ToDB(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            Function dbFunction;
            if (ID != 0)
            {
                dbFunction = dbContext.Functions.FirstOrDefault(m => m.ID == ID);
            } else
            {
                dbFunction = new Function();
            }
            string uniqueFileName = UploadUtils.ProcessUploadedFile(Image, webHostEnvironment);
            dbFunction.ID = ID;
            dbFunction.LongDescription = LongDescription;
            dbFunction.Title = Title;
            if (uniqueFileName != null)
            {
                dbFunction.Image = uniqueFileName;
            }
            return dbFunction;
        }
        public static FunctionViewModel FromDB(Function function)
        {
            return new FunctionViewModel
            {
                ID = function.ID,
                LongDescription = function.LongDescription,
                Title = function.Title,
                ExistingImage = function.Image
            };
        }
        public string ExistingImagePath { get { return UploadUtils.UploadedFilePath(ExistingImage);  } }
    }
}

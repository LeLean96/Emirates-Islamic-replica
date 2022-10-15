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
    public class OfferViewModel
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
        public Offer ToDB(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            Offer dbOffer;
            if (ID != 0)
            {
                dbOffer = dbContext.Offers.FirstOrDefault(m => m.ID == ID);
            }
            else
            {
                dbOffer = new Offer();
            }
            string uniqueFileName = UploadUtils.ProcessUploadedFile(Image, webHostEnvironment);
            dbOffer.ID = ID;
            dbOffer.LongDescription = LongDescription;
            dbOffer.ShortDescription = ShortDescription;
            dbOffer.Title = Title;
            if (uniqueFileName != null)
            {
                dbOffer.Image = uniqueFileName;
            }
            return dbOffer;
        }
        public static OfferViewModel FromDB(Offer offer)
        {
            return new OfferViewModel
            {
                ID = offer.ID,
                LongDescription = offer.LongDescription,
                ShortDescription = offer.ShortDescription,
                Title = offer.Title,
                ExistingImage = offer.Image
            };
        }
        public string ExistingImagePath { get { return UploadUtils.UploadedFilePath(ExistingImage); } }
    }
}

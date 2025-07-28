using DayLifeSure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace DayLifeSure.Controllers
{
    public class TestimonialController : Controller
    {
        // GET: Testimonial
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        public PartialViewResult TestimonialList()
        {
            var currentLang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var values = db.TestimonialTranslations.Include(x=>x.TblTestimonials).Where(x => x.LanguageCode == currentLang).ToList(); 
            return PartialView("_TestimonialList",values);
        }
    }
}
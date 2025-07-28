using DayLifeSure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace DayLifeSure.Controllers
{
    public class FaqController : Controller
    {
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        // GET: Faq
        public PartialViewResult FAQList()
        {
            var currentLang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var values = db.FaqTranslation.Include(x=>x.TblFAQs).Where(x => x.LanguageCode == currentLang).ToList();
            return PartialView("_FAQList",values);
        }
    }
}
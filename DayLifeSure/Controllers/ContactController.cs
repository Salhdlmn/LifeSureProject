using DayLifeSure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayLifeSure.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        public PartialViewResult ContactList()
        {
            var currentLang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var values = db.ContactInfoTranslation.Include(x=>x.TblContactInfo).Where(x=>x.LanguageCode==currentLang).ToList();
            return PartialView("_ContactList",values);
        }
    }
}
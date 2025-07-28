using DayLifeSure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayLifeSure.Controllers
{
    public class AboutController :Controller
    {
        LifeSureDbEntities1 db= new LifeSureDbEntities1();
        public PartialViewResult AboutList()
        {
            var currentLang=System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var values = db.AboutTranslation.Where(x=>x.LanguageCode == currentLang).ToList();
            return PartialView("_AboutList",values);
        }
    }
}
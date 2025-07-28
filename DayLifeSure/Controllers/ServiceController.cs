using DayLifeSure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace DayLifeSure.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        LifeSureDbEntities1 db = new LifeSureDbEntities1 ();    
        public PartialViewResult ServiceList()
        {
            var currentLang =System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var values = db.ServiceTranslation.Include(x=>x.TblServices).Where(x => x.LanguageCode == currentLang).ToList();
            return PartialView("_ServiceList",values);
        }
    }
}
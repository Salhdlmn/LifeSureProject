using DayLifeSure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DayLifeSure.Controllers
{
    public class FeatureController : Controller
    {
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        public PartialViewResult FeatureList()
        {
            var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            var features = db.FeatureTranslation
                             .Include(x => x.TblFeatures) // navigation property'nin doğru adıysa
                             .Where(x => x.LanguageCode == lang)
                             .ToList();

            return PartialView("_FeatureList", features);
        }
    }
}
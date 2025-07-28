using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayLifeSure.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Change(string lang, string returnUrl)
        {
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = lang;
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);

            return Redirect(returnUrl);
        }
    }
}
using DayLifeSure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayLifeSure.Controllers
{
    public class EmployeController : Controller
    {
        // GET: Employe

        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        public PartialViewResult EmployeeList()
        {
            var values= db.TblEmployees.ToList();
            return PartialView("_EmployeeList", values);
        }
    }
}
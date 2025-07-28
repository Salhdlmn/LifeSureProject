using DayLifeSure.Models.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DayLifeSure.Controllers
{
    public class AdminLayoutController : Controller
    {
        // GET: AdminLayout

        LifeSureDbEntities1 db=new LifeSureDbEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PartialAdminHead()
        {
            return PartialView();
        }

        public PartialViewResult PartialAdminAside()
        {
            return PartialView();
        }

        public PartialViewResult PartialAdminNavbar()
        {
            return PartialView();
        }


        public PartialViewResult PartialAdminScript()
        {
            return PartialView();
        }

        public PartialViewResult PartialAdminFooter()
        {
            return PartialView();
        }

        public ActionResult Feature()
        {
            var features = db.TblFeatures.ToList();
            return View(features);
        }

        [HttpGet]
        public ActionResult AddFeature()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFeature(TblFeatures feature)
        {
            db.TblFeatures.Add(feature);
            db.SaveChanges();
            return RedirectToAction("Feature");
        }

        public ActionResult DeleteFeature(int id)
        {
            var feature = db.TblFeatures.Find(id);
            db.TblFeatures.Remove(feature);
            db.SaveChanges();
            return RedirectToAction("Feature");
        }

        [HttpGet]

        public ActionResult UpdateFeature(int id)
        {
            var feature = db.TblFeatures.Find(id);
            return View(feature);
        }

        [HttpPost]

        public ActionResult UpdateFeature(TblFeatures feature)
        {
            var existingFeature = db.TblFeatures.Find(feature.FeatureId);
            existingFeature.FeatureDesc = feature.FeatureDesc;
            existingFeature.FeatureTitle = feature.FeatureTitle;
            existingFeature.FeatureIcon = feature.FeatureIcon;
            db.SaveChanges();
            return RedirectToAction("feature");
            

        }

        public ActionResult Service()
        {
            var features = db.TblServices.ToList();
            return View(features);
        }


        [HttpGet]
        public ActionResult AddService()
        {
            return View();
        }

        [HttpPost]

        public ActionResult AddService(TblServices services)
        {
            db.TblServices.Add(services);
            db.SaveChanges();
            return View("Service");
        }

        public ActionResult DeleteService(int id)
        {
            var service = db.TblServices.Find(id);
            db.TblServices.Remove(service);
            db.SaveChanges();
            return RedirectToAction("Service");
        }


        [HttpGet]

        public ActionResult UpdateService(int id)
        {
            var service = db.TblServices.Find(id);
            return View(service);
        }

        [HttpPost]

        public ActionResult UpdateService(TblServices service)
        {
            var existingFeature = db.TblServices.Find(service.ServiceId);
            existingFeature.ServiceDesc = service.ServiceDesc;
            existingFeature.ServiceTitle= service.ServiceTitle;
            existingFeature.Icon= service.Icon;
            existingFeature.ImageUrl= service.ImageUrl;
            db.SaveChanges();
            return RedirectToAction("service");


        }

        public ActionResult FAQ()
        {
            var values= db.TblFAQs.ToList();
            return View(values);
        }

        [HttpPost]
        public async Task<JsonResult> GenerateByAI()
        {
            var prompt = "Generate 3 frequently asked questions and answers related to insurance (life, health, car, home). Keep the language clear and convincing. Respond in JSON format: [{\"question\": \"...\", \"answer\": \"...\"}]";

            var response = await CallGPTAPI(prompt);

            if (response != null)
            {
                var faqItems = JsonConvert.DeserializeObject<List<TblFAQs>>(response);

                foreach (var item in faqItems)
                {
                    db.TblFAQs.Add(item);
                }

                db.SaveChanges();
                return Json(new { success = true, message = "FAQ(s) successfully added!" });
            }

            return Json(new { success = false, message = "FAQ generation failed." });
        }

        private async Task<string> CallGPTAPI(string prompt)
        {
            var apiKey = " Your_Api_Key"; // Buraya kendi API anahtarını yaz
            var apiUrl = "https://api.openai.com/v1/chat/completions";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                new { role = "system", content = "You are a helpful assistant that outputs JSON." },
                new { role = "user", content = prompt }
            },
                    temperature = 0.7
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    dynamic result = JsonConvert.DeserializeObject(responseString);
                    string text = result.choices[0].message.content;
                    return text;
                }

                return null;
            }
        }


        public ActionResult About()
        {
            var about = db.TblAbouts.ToList();
            return View(about);

        }

    }
}
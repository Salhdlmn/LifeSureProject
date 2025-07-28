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
using DayLifeSure.Models.ViewModels;

namespace DayLifeSure.Controllers
{
    public class GenerateImageController : Controller
    {
        // GET: Image

        private readonly string _apiKey = "Your_Api_Key";
        [HttpGet]
        public ActionResult ImageGenerate()
        {
            return View(new GeneratedImageViewModel());
        }
        [HttpPost]
        public async Task<ActionResult> ImageGenerate(GeneratedImageViewModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.Prompt))
            {
                ModelState.AddModelError("", "Prompt alanı boş olamaz.");
                return View(model);
            }

            string imageBase64 = await GenerateImageFromPrompt(model.Prompt);

            if (!string.IsNullOrEmpty(imageBase64))
            {
                model.ImageBase64 = imageBase64;
            }
            else
            {
                ModelState.AddModelError("", "Görsel oluşturulamadı.");
            }

            return View(model);
        }


        private async Task<string> GenerateImageFromPrompt(string prompt)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _apiKey);

                var data = new
                {
                    inputs = prompt
                };

                var json = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(data),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(
                    "https://api-inference.huggingface.co/models/black-forest-labs/FLUX.1-dev",
                    json
                );

                var responseBytes = await response.Content.ReadAsByteArrayAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = Encoding.UTF8.GetString(responseBytes);
                    throw new Exception("API başarısız: " + errorMsg);
                }

                try
                {
                    var responseString = Encoding.UTF8.GetString(responseBytes);

                    dynamic jsonObj = JsonConvert.DeserializeObject(responseString);

                    // Eğer JSON bir listeyse (genellikle [{ "generated_image": "base64" }])
                    string base64Image = jsonObj[0]?.generated_image ?? jsonObj.generated_image ?? null;

                    if (!string.IsNullOrEmpty(base64Image))
                        return base64Image;
                }
                catch
                {
                    // JSON parse edilemediyse doğrudan base64 olduğunu varsay
                    return Convert.ToBase64String(responseBytes);
                }

                throw new Exception("Beklenmeyen API cevabı.");
            }
        }

    }
}
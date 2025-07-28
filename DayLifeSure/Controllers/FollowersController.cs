using DayLifeSure.Models.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DayLifeSure.Controllers
{
    public class FollowersController : Controller
    {
        // GET: Followers
        private readonly string apiUrl = "https://fresh-linkedin-scraper-api.p.rapidapi.com/api/v1/user/follower-and-connection?username={0}";
        private readonly string apiKey = " 6a94c392f3mshdecdb76b36b74b0p10dbfejsn3af890425c40";
        private readonly string apiHost = "fresh-linkedin-scraper-api.p.rapidapi.com";
        public PartialViewResult LinkedinFollower()
        {
            string username = "salihdilmen";
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format(apiUrl, username)),
            };
            request.Headers.Add("x-rapidapi-key", apiKey);
            request.Headers.Add("x-rapidapi-host", apiHost);

            var response = client.SendAsync(request).Result; // Zaman uyumsuz değil!
            var body = response.Content.ReadAsStringAsync().Result;

            var obj = JObject.Parse(body);
            var data = obj["data"];

            var viewModel = new LinkedinViewModel
            {
                Username = username,
                FollowersCount = (int)(data?["follower_count"] ?? 0),
                PublicIdentifier = (string)(data?["public_identifier"] ?? username)
            };

            return PartialView("_LinkedinFollower", viewModel);
        }

    }
}
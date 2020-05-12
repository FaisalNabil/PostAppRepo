using Newtonsoft.Json;
using PostApp.Service.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PostsApp.Controllers
{
    public class HomeController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["baseurl"] + "/api/";
        HttpClient client;
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public ActionResult Index(int pageIndex = 1)
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public async Task<ActionResult> Posts(int pageid)
        {
            List<PostModel> posts = new List<PostModel>();
            HttpResponseMessage responseMessage = await client.GetAsync(apiUrl + "Post?page=" + pageid);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<List<PostModel>>(responseData);
            }
            return View(posts);
        }
    }
}

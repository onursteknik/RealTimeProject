using RTP.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace RTP.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home https://data.dublinked.ie/cgi-bin/rtpi/realtimebusinformation?stopid=103381&format=xml
        public async Task<ActionResult> Index()
        {
            if (HttpContext.Cache["InformationData"] == null)
            {
                using (var client = new HttpClient())
                {
                    List<InformationReport> infoData = null;
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseApiAddress"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("api/Data/GetInformations");
                    if (response.IsSuccessStatusCode)
                    {
                        infoData = await response.Content.ReadAsAsync<List<InformationReport>>();
                        HttpContext.Cache.Insert("InformationData", infoData, null, DateTime.UtcNow.AddSeconds(30), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
                    }
                    return View(infoData);
                }
            }
            else
            {
                var data = (List<InformationReport>)HttpContext.Cache["InformationData"];
                return View(data);
            }
        }
        [HttpGet]
        public async Task<ActionResult> InformationContent()
        {
            if (HttpContext.Cache["InformationData"] == null)
            {
                using (var client = new HttpClient())
                {
                    List<InformationReport> infoData = null;
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseApiAddress"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("api/Data/GetInformations");
                    if (response.IsSuccessStatusCode)
                    {
                        infoData = await response.Content.ReadAsAsync<List<InformationReport>>();
                        HttpContext.Cache.Insert("InformationData", infoData, null, DateTime.UtcNow.AddSeconds(30), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
                    }
                    return View(infoData);
                }
            }
            else
            {
                var data = (List<InformationReport>)HttpContext.Cache["InformationData"];
                return View(data);
            }
        }
    }
}
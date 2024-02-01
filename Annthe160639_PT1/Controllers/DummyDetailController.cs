using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebAPI.Models;

namespace Annt_PT1.Controllers
{
    public class DummyDetailController : Controller
    {
        private readonly string _baseUrl = "https://localhost:7283/api/DummyDetail";

        public async Task<IActionResult> Index(string Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.Message = Message;
            }
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_baseUrl);

                var responseJson = await response.Content.ReadAsStringAsync();

                var list = JsonConvert.DeserializeObject<List<DummyDetail>>(responseJson);

                response = await client.GetAsync("https://localhost:7283/api/DummyMaster");

                responseJson = await response.Content.ReadAsStringAsync();

                var listmaster = JsonConvert.DeserializeObject<List<DummyMaster>>(responseJson);
                ViewBag.Master = listmaster;
                return View(list);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Filter(string? detailName, string? master)
        {
            List<DummyDetail> list1 = null;
            List<DummyDetail> list2 = null;
            string detail = detailName ==  null ? "%20" : detailName;
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(_baseUrl + "/" + detail);


                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();

                        list1 = JsonConvert.DeserializeObject<List<DummyDetail>>(responseJson);
                    }
                    else
                    {
                        var errorResponseJson = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("Index", new { Message = errorResponseJson });

                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View();
            }

            string masterName = master == null ? "%20" : master;
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(_baseUrl + "/Master/" + masterName);


                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();

                        list2 = JsonConvert.DeserializeObject<List<DummyDetail>>(responseJson);
                    }
                    else
                    {
                        var errorResponseJson = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("Index", new { Message = errorResponseJson });

                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View();
            }
            HashSet<int> idSet = new HashSet<int>(list2.Select(obj => obj.DetailId));
            var result = new List<DummyDetail>();
            foreach(var e in list1)
            {
                if (idSet.Contains(e.DetailId))
                {
                    result.Add(e);
                }
            }
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7283/api/DummyMaster");

                var responseJson = await response.Content.ReadAsStringAsync();

                var listmaster = JsonConvert.DeserializeObject<List<DummyMaster>>(responseJson);
                ViewBag.Master = listmaster;
                if (listmaster.Count == 0)
                {
                    ViewBag.Message = "Not found";
                }
                ViewBag.a = detailName;
                ViewBag.a = master;
            }
            return View("Index", result);
        }
    }
}

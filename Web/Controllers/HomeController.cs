using API.SDK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiClient _client;

        public HomeController(ILogger<HomeController> logger, ApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var comparisons = await _client.Comparisons.GetAll();

            var model = new ComparisonListViewModel()
            {
                Comparisons = comparisons
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ComparisonListViewModel model)
        {
            Guid guid = new Guid();
            if (model.ID.HasValue)
            {
                guid = model.ID.Value;
            }
            else
            {
                guid = Guid.NewGuid(); 
            }
            if(model.RightSide != null)
            {
                try
                {
                    await _client.Comparisons.AddRight(guid, model.RightSide);
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
            if(model.LeftSide != null)
            {
                try
                {
                    await _client.Comparisons.AddLeft(guid, model.LeftSide);
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

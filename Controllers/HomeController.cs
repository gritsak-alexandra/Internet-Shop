using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shhop.Models;
using shhop.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace shhop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index(string category) //вывод главной странички
        {
            string _category = category; //категория
            string _categoryItem = null; //товар в категории
            IEnumerable<Thing> things = null; //все товары
            string currCategory = ""; //текущая категория
            string currCategoryItem = ""; //товар в текущей категории
            if (string.IsNullOrEmpty(category))
            {
                things = context.Thing.OrderBy(t => t.id);

            }
            else
            {
                things = context.Thing.Where(t => t.categoryId == int.Parse(category)).OrderBy(t => t.id);
            }

            var thingObj = new ThingListViewModel //вывод 
            {
                allThings = things,
                currCategory = currCategory,
                currCategoryItem = currCategoryItem
            };
            ViewBag.Title = "Page of things";
            return View(thingObj);
        }

        public IActionResult Privacy() //вывод privacy
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

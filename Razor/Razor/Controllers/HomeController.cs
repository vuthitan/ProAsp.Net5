using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Razor.Models;

namespace Razor.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        Product product = new Product
        {
            ProductId = 1,
            Name = "Kayak",
            Description = "A boat for one person",
            Category = "Watersports",
            Price = 275M
        };

        public ActionResult Index()
        {
            return View(product);
        }

        public ActionResult NameAndPrice()
        {
            return View(product);
        }

        public ActionResult DemoExpression()
        {
            ViewBag.ProductCount = 1;
            ViewBag.ExpressShip = true;
            ViewBag.ApplyDiscount = false;
            ViewBag.Supplier = null;

            return View(product);
        }

        public ActionResult DemoArray()
        {
            Product[] arrays =
            {
                new Product {Name = "Kayak", Price= 275},
                new Product {Name= "Lifejacket", Price= 48.95M},
                new Product {Name= "Soccer ball", Price= 19.50M},
                new Product {Name="Corner flag", Price= 34.95M}
            };
            return View(arrays);
        }
    }
}
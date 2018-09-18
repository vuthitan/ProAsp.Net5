using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFeatures.Models;
using System.Text;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navigate to a URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            Product myProduct = new Product();
            myProduct.Name = "Kayak";
            string productName = myProduct.Name;
            return View("Result",
                    (object)String.Format("Product name: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            Product myProduct = new Product();
            myProduct.ProductID = 100;
            myProduct.Name = "Kayak";
            myProduct.Description = "A boat for one person";
            myProduct.Price = 275M;
            myProduct.Category = "Watersports";

            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));
        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = { "apple", "orange", "plum" };
            List<int> intList = new List<int> { 10, 20, 30, 40 };
            Dictionary<string, int> myDict = new Dictionary<string, int>
            {
                {"apple", 10 }, {"orange", 20}, {"plum", 40}
            };
            return View("Result", (object)stringArray[1]);
        }

        public ViewResult UseExtension()
        {
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product{Name = "Kayak", Price = 275M},
                    new Product{Name = "Lifejacket", Price = 48.5M},
                    new Product{Name = "Soccer ball", Price = 19.50M},
                    new Product{Name = "Corner flag", Price = 34.95M}
                }
            };
            decimal cartTotal = cart.TotalPrice();
            return View("Result", (object)String.Format("Total: {0:c}", cartTotal));
        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product{Name = "Kayak", Price = 275M},
                    new Product{Name = "Lifejacket", Price = 48.5M},
                    new Product{Name = "Soccer ball", Price = 19.50M},
                    new Product{Name = "Corner flag", Price = 34.95M}
                }
            };
            Product[] productArray =
            {
                new Product{Name = "Kayak", Price = 275M},
                new Product{Name = "Lifejacket", Price = 48.5M},
                new Product{Name = "Soccer ball", Price = 19.50M},
                new Product{Name = "Corner flag", Price = 34.95M}
            };
            decimal cartTotal = products.TotalPrice();
            decimal arrayTotal = productArray.TotalPrice();
            return View("Result", (object)String.Format("Cart Total: {0}, Array Total: {1}", cartTotal, arrayTotal));
        }

        public ViewResult UseFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product{Name = "Kayak", Price = 275M},
                    new Product{Name = "Lifejacket", Price = 48.5M},
                    new Product{Name = "Soccer ball", Price = 19.50M},
                    new Product{Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                }
            };

            //Func<Product, bool> categoryFilter = delegate (Product product)
            //{
            //    return product.Category == "Soccer";
            //};

            Func<Product, bool> categoryFilter = prod => prod.Category == "Soccer";

            decimal total = 0;
            //foreach (Product product in products.Filter(categoryFilter)) {
            //    total += product.Price;
            //}
            foreach (Product prod in products.Filter(prod => prod.Category == "Soccer"
                                                             || prod.Price <= 20))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format("Total: {0}", total));
        }

        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new { Name = "MVC", Category = "Pattern"},
                new { Name = "Hat", Category = "Clothing"},
                new { Name = "Apple", Category = "Fruit"}
            };
            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append("");
            }
            return View("Result", (object)result.ToString());
        }

        public ViewResult Fruit()
        {
            var fruits = new[]
            {
                new { Name = "Apple", price = 20},
                new { Name = "Grape", price = 10},
                new { Name = "Papaya", price = 30},
                new { Name = "Strawberry", price = 25}
            };
            var found = fruits.OrderByDescending(e => e.price)
                            .Take(3)
                            .Select(e => new { e.Name, e.price });
            StringBuilder result = new StringBuilder();
            foreach (var item in found)
            {
                result.Append(item.Name).Append("");
            }
            return View("Result", (object)result.ToString());
        }
    }
}
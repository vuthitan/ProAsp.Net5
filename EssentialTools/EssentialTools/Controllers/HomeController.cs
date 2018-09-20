﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        private IValueCalculator calc;
        private Product[] products =
        {
            new Product {Name= "Kayak", Category = "Watersprots", Price = 275M},
            new Product {Name= "Lifejacket", Category= "Watersprots", Price = 48.95M},
            new Product {Name= "Soccer ball", Category= "Soccer", Price = 19.50M},
            new Product {Name= "Corner flag", Category= "Soccer", Price = 34.95M}
        };

        public HomeController(IValueCalculator calcParam)
        {
            calc = calcParam;
        }

        //public HomeController(IValueCalculator calcParam, IValueCalculator calc2)
        //{
        //    calc = calcParam;
        //}

        public ActionResult Index()
        {
            //IKernel ninjectKernel = new StandardKernel();
            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            //IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();

            ShoppingCart cart = new ShoppingCart(calc) { Products = products };
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}
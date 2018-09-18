using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Party.Models;

namespace Party.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Morning" : "Afternoon";
            return View();
        }

        [HttpGet] //tell MVC that this method should be used only for GET requests
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost] //tell MVC that the new method will deal with POST requests.
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if(ModelState.IsValid)
            {
                //Todo: email response to the party organizer
                return View("Thanks", guestResponse);
            }
            else
            {
                //thereis a validation error
                return View();
            }
        }
    }
}
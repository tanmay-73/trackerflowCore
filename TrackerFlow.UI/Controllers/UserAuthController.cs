using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrackerFlow.UI.Models;

namespace TrackerFlow.UI.Controllers
{
    public class UserAuthController : Controller
    {    

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
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
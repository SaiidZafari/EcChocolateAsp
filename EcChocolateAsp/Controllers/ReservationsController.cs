using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Controllers
{
    public class ReservationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

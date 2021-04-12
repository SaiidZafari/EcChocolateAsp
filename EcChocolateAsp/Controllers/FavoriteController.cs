using EcChocolateAsp.Extensions;
using EcChocolateAsp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly AppDbContext context;
        public IActionResult Index()
        {
            var favorite = HttpContext.Session.Get<Favorite>("Favorite") ?? new Favorite();
            return View(favorite);
        }

        public FavoriteController(AppDbContext context)
        {
            this.context = context;
        }


        [Route("/favorite/add", Name = "AddToFavorite")]
        [HttpPost]
        public IActionResult AddToFavorite(int productId)
        {
            var product = context.Products.Include(x => x.Images).First(x => x.Id == productId);

            //After session get is terminated , favorite var is setted as new Favorite object.
            var favorite = HttpContext.Session.Get<Favorite>("Favorite") ?? new Favorite();

            favorite.AddProduct(product);

            HttpContext.Session.Set("Favorite", favorite);

            return RedirectToAction(nameof(Index));
        }
    }

}


using EcChocolateAsp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Controllers
{
    public class ProductsController : Controller
    {

        private readonly AppDbContext context;

        public ProductsController (AppDbContext context)
        {
            this.context = context;
        }

        

        public async Task<IActionResult> Index()
        {
            var products = await context.Products.Include(x => x.Images).ToListAsync();


            var viewModel = new ProductIndexViewModel {
                Products = products,
                PromotedProduct = products.First(x => x.Name == "Classic Ice Cream Cake")
            };


            return View(viewModel);
        }

        [Route("products/{urlSlug}")]
        public IActionResult Display(string urlSlug)
        {
            var product = context.Products.Include(x => x.Images).ToList().FirstOrDefault(product => NameAsUrlSlug(product.Name) == urlSlug);


            // Load reviews from database
            //var reviews = context.Reviews.Include(x => x.Product).ToList().FirstOrDefault(review => review.Product == product);
            var reviews = new List<Review>{
                new Review(product, "test user", "Lorem ipsum dolor sit amet", "Lorem ipsum dolor sit amet consectetur adipisicing elit. Voluptate numquam atque minima pariatur corporis sequi.", 4),
                new Review(product, "test user2", "Lorem ipsum dolor sit amet.", "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Eum deleniti impedit necessitatibus saepe consequatur. Quae, quam vel velit expedita perspiciatis temporibus ullam totam ea veniam beatae ipsa corrupti minus nulla sint fugit reprehenderit.", 4)
            };

            var viewModel = new ProductsDisplayViewModel { 
                Product = product, 
                Reviews = reviews 
            };

            return View(viewModel);
        }

        private string NameAsUrlSlug(string name)
        {
            return name.Trim().Replace("-","").Replace(" ", "-").ToLower();
        }
    }
}

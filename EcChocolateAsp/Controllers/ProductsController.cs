using EcChocolateAsp.Extensions;
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

        public ProductsController(AppDbContext context)
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
            var product = context.Products.Include(x => x.Images).Include(x => x.Reviews).ToList().FirstOrDefault(product => product.Name.Slugify() == urlSlug);


            // Load reviews from database
            //var reviews = context.Reviews.Include(x => x.Product).ToList().FirstOrDefault(review => review.Product == product);
            //var reviews = new List<Review>{
            //    new Review("test user", "Lorem ipsum dolor sit amet", "Lorem ipsum dolor sit amet consectetur adipisicing elit. Voluptate numquam atque minima pariatur corporis sequi.", 4),
            //    new Review("test user2", "Lorem ipsum dolor sit amet.", "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Eum deleniti impedit necessitatibus saepe consequatur. Quae, quam vel velit expedita perspiciatis temporibus ullam totam ea veniam beatae ipsa corrupti minus nulla sint fugit reprehenderit.", 4)
            //};
            var dto = new ReviewsDto {
                Reviews = product.Reviews.ToList(),
                ProductId = product.Id
            };

            var viewModel = new ProductsDisplayViewModel {
                Product = product,
                ReviewsDto = dto
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("review/add")]
        public IActionResult Review(ProductsDisplayViewModel viewModel)
        {
            // Parse dto -> save review to database -> return to same page with review added
            var product = context.Products.Include(x => x.Reviews).First(x => x.Id == viewModel.ReviewsDto.ProductId);
            var review = new Review(viewModel.ReviewsDto.UserName, viewModel.ReviewsDto.ReviewTitle, viewModel.ReviewsDto.ReviewBody, viewModel.ReviewsDto.ReviewScore);
            product.Reviews.Add(review);
            //context.Reviews.Add(review);
            context.Update(product);
            context.SaveChanges();

            return Redirect($"/products/{product.Name.Slugify()}");
        }
    }
}

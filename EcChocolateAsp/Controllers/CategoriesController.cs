using EcChocolateAsp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext context;

        public CategoriesController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Categories.ToListAsync());
        }

        [Route("categories/{urlSlug}")]
        public IActionResult Display(string urlSlug)
        {
            var categoryId = urlSlug.Split('-').First();
            var products = context.Products.Where(product => product.Category.Id == int.Parse(categoryId)).ToList();

            return View(products);
        }

        private string NameAsUrlSlug(string name)
        {
            return name.Trim().Replace(" ", "+").ToLower();
        }
    }
}

﻿using EcChocolateAsp.Models;
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
            return View(await context.Products.ToListAsync());
        }

        [Route("products/{urlSlug}")]
        public IActionResult Display(string urlSlug)
        {
            var product = context.Products.FirstOrDefault(product => NameAsUrlSlug(product.Name) == urlSlug);

            return View(product);
        }

        private string NameAsUrlSlug(string name)
        {
            return name.Trim().Replace(" ", "+").ToLower();
        }
    }
}

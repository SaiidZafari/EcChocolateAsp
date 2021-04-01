using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcChocolateAsp.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using EcChocolateAsp.Areas.Admin.Models;
using Newtonsoft.Json;

namespace EcChocolateAsp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext context;

        public ProductsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: Admin/Products
        [Route("Admin/Products", Name = "adminProducts")]
        public async Task<IActionResult> Index()
        {
            return View(await context.Products.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                context.Add(product);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(product);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await context.Products.FindAsync(id);
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [Route("Admin/Products/Import")]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/Products/Import")]
        public IActionResult Import(IFormFile file)
        {

            List<int> Ids = context.Products.Select(prod => prod.Id).ToList();

            var content = string.Empty;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                content = reader.ReadToEnd();
            }

            List<ImportProductModel> productItems = null;

            try
            {
                productItems = JsonConvert.DeserializeObject<List<ImportProductModel>>(content);
            }
            catch (Exception)
            {

                return BadRequest();
            }

            foreach (var productItem in productItems)
            {
                var images = new List<ImageUrl>();
                foreach (var item in productItem.Images)
                {
                    images.Add(new ImageUrl(item));
                }
                if(Ids.Contains(productItem.Id))
                {

                    context.Update(new Product(productItem.Id, productItem.Name, productItem.Price, productItem.Description, images));
                }
                else
                {

                    context.Add(new Product(productItem.Name, productItem.Price, productItem.Description, images));
                }

            }

            context.SaveChanges();

            //RedirectToRoute()
            return RedirectToRoute("adminProducts");
            //return RedirectToAction("index", "products");
        }


        private bool ProductExists(int id)
        {
            return context.Products.Any(e => e.Id == id);
        }
    }
}

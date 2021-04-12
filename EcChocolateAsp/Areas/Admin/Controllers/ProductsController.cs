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
        public async Task<IActionResult> Create(CreateProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = null;
                if (viewModel.Images.Count == 0)
                {
                    newProduct = new Product(viewModel.Name, viewModel.Price, viewModel.Description);
                }
                else if (viewModel.Images.Count == 1)
                {
                    newProduct = new Product(viewModel.Name, viewModel.Price, viewModel.Description, viewModel.Images.First());
                }
                else
                {
                    var images = new List<ImageUrl>();
                    foreach (var url in viewModel.Images)
                    {
                        images.Add(new ImageUrl(url));
                    }

                    newProduct = new Product(viewModel.Name, viewModel.Price, viewModel.Description, images);
                }
                context.Add(newProduct);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
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

            var viewModel = new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };

            return View(viewModel);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var images = new List<ImageUrl>();

                    var product = await context.Products.FindAsync(id);
                    await context.Entry(product).Collection(x => x.Images).LoadAsync();

                    var newProduct = new Product(viewModel.Id, viewModel.Name, viewModel.Price, viewModel.Description, product.Images);
                    context.Update(newProduct);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(viewModel.Id))
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
            return View(viewModel);
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
                if (Ids.Contains(productItem.Id))
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

        public IActionResult Images(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = context.Products
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            context.Entry(product).Collection(x => x.Images).Load();

            string textAreaData = "";

            foreach (var imageUrl in product.Images)
            {
                textAreaData += $"{imageUrl.Url}\r\n";
            }

            var viewModel = new ProductImagesViewModel
            {
                Id = product.Id,
                TextAreaData = textAreaData
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Images(int id, ProductImagesViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            var product = context.Products.Include(x => x.Images)
                .FirstOrDefault(m => m.Id == id);

            var images = new List<ImageUrl>();

            foreach (var line in viewModel.TextAreaData.Split("\r\n"))
            {
                var image = product.Images.FirstOrDefault(x => x.Url == line);
                if (image == null)
                {
                    images.Add(new ImageUrl(line));
                }
                else
                {
                    images.Add(image);
                }
            }

            var updatedProduct = new Product(product.Id, product.Name, product.Price, product.Description, images);

            try
            {
                context.Update(updatedProduct);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!ProductExists(updatedProduct.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToRoute("adminProducts");
        }






        private bool ProductExists(int id)
        {
            return context.Products.Any(e => e.Id == id);
        }
    }
}

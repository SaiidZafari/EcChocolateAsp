using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcChocolateAsp.Models
{
    public class Favorite
    {
        public IList<ProductFavorite> Items { get; set; } = new List<ProductFavorite>();

        public void AddProduct(Product product)
        {
            var favItem = Items.FirstOrDefault(x => x.Id == product.Id);

            if (favItem == null)
            {
                Items.Add(new ProductFavorite
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Image = product.Image
                });
            }
        }
    }


    public class ProductFavorite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }

}

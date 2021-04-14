using EcChocolateAsp.Models;
using System.Collections.Generic;

namespace EcChocolateAsp.Areas.Admin.Models
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public IEnumerable<Product> Products { get; protected set; } = new List<Product>();
        // TODO products and image
    }
}
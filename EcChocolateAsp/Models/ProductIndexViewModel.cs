using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Models
{
    public class ProductIndexViewModel
    {

        public IEnumerable<Product> Products { get; set; }

        public Product PromotedProduct { get; set; }
    }
}

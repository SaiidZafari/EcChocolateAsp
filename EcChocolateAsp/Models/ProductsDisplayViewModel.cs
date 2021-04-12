using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Models
{
    public class ProductsDisplayViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Review> Reviews { get; set; } = new List<Review>();
    }
}

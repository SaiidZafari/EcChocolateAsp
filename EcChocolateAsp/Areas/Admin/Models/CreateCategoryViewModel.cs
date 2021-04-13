using EcChocolateAsp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Areas.Admin.Models
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; protected set; } = new List<Product>();
    }
}

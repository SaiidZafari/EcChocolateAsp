using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Models
{
    public class Category
    {
        public Category(string name)
        {
            Name = name;
        }

        public Category(string name, IEnumerable<Product> products)
            :this(name)
        {
            Products = products;
        }

        public Category(int id, string name, IEnumerable<Product> products)
            :this(name, products)
        {
            Id = id;
        }




        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public IEnumerable<Product> Products { get; protected set; } = new List<Product>();
    }
}

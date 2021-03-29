using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Models
{
    public class Product
    {
        public Product(int id, string name, decimal price, string description, ICollection<ImageUrl> images)
            :this(name, price, description)
        {
            Id = id;
            Images = images;
        }

        public Product(string name, decimal price, string description, string imageUrl)
            : this(name, price, description)
        {
            Images = new List<ImageUrl> { new ImageUrl(imageUrl) };
        }

        public Product(string name, decimal price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }


        public int Id { get; protected set; }

        public string Name { get; protected set; }

        // We have to discuss this
        // can products be in multiple categories? (many-to-many)
        // YES:
        //public IEnumerable<Category> Categories { get; protected set; } = new List<Category>();
        // or
        // NO:
        //public Category Category { get; protected set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; protected set; }

        public string Description { get; protected set; }

        public ICollection<ImageUrl> Images { get; protected set; } = new List<ImageUrl>();


        // We cound implement it this way, not sure if we should
        //public string Image_path_desktop { get => (Images.Count > 0 ? Images[0] : ""); }
        //public string Image_path_tablet { get => (Images.Count > 1 ? Images[1] : Image_path_desktop); }
        //public string Image_path_mobile { get => (Images.Count > 2 ? Images[2] : Image_path_tablet); }
    }
}

﻿using System;
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

        public Product(string name, decimal price, string description, ICollection<ImageUrl> images)
            : this(name, price, description)
        {
            Images = images;
        }

        public Product(string name, decimal price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }


        public int Id { get; protected set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; protected set; }

        //public Category Category { get; protected set; }
        /// <summary>
        /// Price of the product in SEK
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; protected set; }

        /// <summary>
        /// Description of the product
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// A collection of image names, need to be prefixed by /images/ when adding a path (for legacy reasons)
        /// </summary>
        public ICollection<ImageUrl> Images { get; protected set; } = new List<ImageUrl>();


        // We cound implement it this way, not sure if we should
        //public string Image_path_desktop { get => (Images.Count > 0 ? Images[0] : ""); }
        //public string Image_path_tablet { get => (Images.Count > 1 ? Images[1] : Image_path_desktop); }
        //public string Image_path_mobile { get => (Images.Count > 2 ? Images[2] : Image_path_tablet); }
    }
}

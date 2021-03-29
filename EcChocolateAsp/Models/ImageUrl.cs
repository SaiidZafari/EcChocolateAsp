using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Models
{
    public class ImageUrl
    {
        public ImageUrl(string url)
        {
            Url = url;
        }

        public ImageUrl(int id, string url)
            :this(url)
        {
            Id = id;
        }

        public int Id { get; protected set; }

        public string Url { get; protected set; }

    }
}

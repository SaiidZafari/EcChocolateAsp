using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcChocolateAsp.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EcChocolateAsp.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Reservation> Reservations  { get; set; }

        public DbSet<EcChocolateAsp.ViewModels.RegisterViewModel> RegisterViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }


        //public DbSet<ImageUrl> ImageUrls { get; set; }

    }
}

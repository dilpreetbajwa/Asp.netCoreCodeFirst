using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

    public class ProductDBContext : DbContext
    {
        public ProductDBContext (DbContextOptions<ProductDBContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplication2.Models.Product> Product { get; set; } = default!;
        public DbSet<WebApplication2.Models.ProductUser> ProductUser { get; set; } = default!;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Test.Products.Database.Models;

namespace Test.Products.Database
{
    public class ProductsDBContext : DbContext
    {
        public ProductsDBContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}

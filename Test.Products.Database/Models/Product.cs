using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Test.Products.Database.Models
{
    //public class Product
    public class Product : DropCreateDatabaseAlways<ProductsDBContext>
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

        public string About { get; set; }

        public string Image { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Products.Database.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public int Price { get; set; }

        public string About { get; set; }

        public string Image { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Product
    {
        public Product()
        {
            DeleteStatus = false;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public bool DeleteStatus { get; set; }

        public DateTime RegDate { get; set; }

        public int Stock { get; set; } //How many product is left

        public List<Invoice> Invoices { get; set; } =  new List<Invoice>();
    }
}

using StockManagementProject.Entities.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Entities.Entities.Concreate
{
    public class Supplier:BaseEntity
    {
        public Supplier()
        {
           
            this.Products = new List<Product>();
        }
        public string SuppplierName { get; set; }

        public string Email { get; set; }

        public string? Adress { get; set; }

        public string? Phone { get; set; }


        public virtual List<Product>? Products { get; set; }
     
    }
}

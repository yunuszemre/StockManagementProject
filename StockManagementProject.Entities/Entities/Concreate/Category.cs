using StockManagementProject.Entities.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Entities.Entities.Concreate
{
    public  class Category:BaseEntity
    {
        public Category()
        {
            this.Products = new List<Product>();
        }
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public virtual List<Product>? Products { get; set; }
    }
}

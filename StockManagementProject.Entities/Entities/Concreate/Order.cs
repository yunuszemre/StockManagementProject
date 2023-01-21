using StockManagementProject.Entities.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Entities.Entities.Concreate
{
    public class Order:BaseEntity
    {
        public Order()
        {
            this.OrderDetails = new List<OrderDetails>();
        }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User? User { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual List<OrderDetails>? OrderDetails { get; set; }
    }
}

using StockManagementProject.Entities.Entities.Abstract;
using StockManagementProject.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Entities.Entities.Concreate
{
    public class User : BaseEntity
    {
        public User()
        {
            this.Orders = new List<Order>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Adress { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }
        public string Password { get; set; }
        public string? PhotoUrl { get; set; }


        public virtual List<Order>? Orders { get; set; }
    }
}

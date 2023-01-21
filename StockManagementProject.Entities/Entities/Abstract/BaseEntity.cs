using StockManagementProject.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Entities.Entities.Abstract
{
    public abstract class BaseEntity
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        public Status Status { get; set; }
    }
}

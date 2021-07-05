using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsSale.Models.Turnover
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        [Column(TypeName = "decimal(18,5)")]
        public decimal Cost { get; set; }
        public bool IsPaid { get; set; } = false;

        public int? PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}

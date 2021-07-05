using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsSale.Models.Turnover
{
    public class Announcement
    {
        public int Id { get; set; }

        public Announcement() => AnnouncementStatus = new();

        [Required]
        public string AccountEmail { get; set; }
        [Required]
        public string AccountPassword { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal Cost { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsVip { get; set; }
        public bool IsPaid { get; set; }

        public AnnouncementStatus AnnouncementStatus { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public int? PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}

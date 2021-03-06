using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsSale.ViewModels
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float Cost { get; set; }

        [Required]
        public string AccountPassword { get; set; }
        [Required]
        public string AccountLogin { get; set; }

        public bool IsVip { get; set; }
    }
}

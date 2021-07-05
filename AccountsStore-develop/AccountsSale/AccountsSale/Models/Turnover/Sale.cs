using AccountsSale.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsSale.Models.Turnover
{
    public class Sale
    {
        public int Id { get; set; }
        
        public Sale()
        {
            Announcements = new List<Announcement>();
        }

        public List<Announcement> Announcements { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}

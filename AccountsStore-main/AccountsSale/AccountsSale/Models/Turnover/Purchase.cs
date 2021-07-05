using AccountsSale.Models.Users;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsSale.Models.Turnover
{
    public class Purchase
    {
        public int Id { get; set; }

        public Purchase()
        {
            Announcements = new List<Announcement>();
        }

        public List<Announcement> Announcements { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}

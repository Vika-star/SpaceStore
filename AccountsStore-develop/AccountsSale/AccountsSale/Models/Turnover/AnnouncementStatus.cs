using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsSale.Models.Turnover
{
    public class AnnouncementStatus
    {
        public int Id { get; set; }
        
        public bool IsApproved { get; set; }
        public bool IsChecked { get; set; }
        public bool IsRejected { get; set; }

        public Announcement Announcement { get; set; }
        public int? AnnouncementId { get; set; }

    }
}

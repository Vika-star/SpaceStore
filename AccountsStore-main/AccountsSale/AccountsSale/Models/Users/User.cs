using AccountsSale.Models.Turnover;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AccountsSale.Models.Users
{
    public class User : IdentityUser
    {
        public User()
        {
            Purchase = new Purchase();
            Sale = new Sale
            {
                User = this,
                UserId = this.Id,
            };
        }

        public Purchase Purchase { get; set; }
        public Sale Sale { get; set; }
    }
}

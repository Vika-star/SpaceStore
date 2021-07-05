using AccountsSale.Models;
using AccountsSale.Models.Turnover;
using AccountsSale.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AccountsSale.Controllers
{
    [Authorize]
    public class PersonalArea : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        public PersonalArea(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PersonalData()
        {
            return View();
        }
        public IActionResult PayConfirmation(int? id)
        {
            if (id == null)
                return NotFound();
            return View(id);
        }
        public async Task<IActionResult> Purchases()
        {
            var user = await _userManager.GetUserAsync(User);
            var purchase = await _context.Purchases
                .Include(x=>x.Announcements)
                .ThenInclude(x=>x.Sale)
                .ThenInclude(x=>x.User)
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id));

            return View(purchase);
        }

        public async Task<IActionResult> Purchase(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var purchase = await _context.Purchases
                .Include(x => x.Announcements)
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id));

            var announcement = await _context.Announcements.FindAsync(id);

            announcement.IsPaid = true;

            purchase.Announcements.Add(announcement);
            await _context.SaveChangesAsync();

            return RedirectToAction("Purchases", "PersonalArea");
        }

        public async Task<IActionResult> Details(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            return View(announcement);
        }
    }
}

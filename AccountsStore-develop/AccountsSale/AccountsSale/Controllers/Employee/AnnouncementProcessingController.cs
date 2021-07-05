using AccountsSale.Models;
using AccountsSale.Models.Turnover;
using AccountsSale.Models.Users;

using CosmeticShop.Models.PDF.Check;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AccountsSale.Controllers
{
    [Authorize(Roles = "employee")]
    public class AnnouncementProcessingController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        public AnnouncementProcessingController(ApplicationContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var annoucements = await _context.Announcements
                .Include(x => x.Sale)
                .ThenInclude(x => x.User)
                .Include(x => x.AnnouncementStatus)
                .Where(x => !x.AnnouncementStatus.IsRejected && !x.AnnouncementStatus.IsChecked)
                .ToListAsync();

            return View(annoucements);
        }

        public async Task<IActionResult> Process(int? id)
        {
            var announcement = await _context.Announcements
                .Include(x => x.AnnouncementStatus)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (announcement == null)
                return NotFound();

            return View(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> Process(AnnouncementStatus status)
        {
            var announcement = await _context.Announcements
                .Include(x => x.AnnouncementStatus)
                .FirstOrDefaultAsync(x => x.Id.Equals(status.AnnouncementId));

            if (status.IsRejected)
            {
                announcement.AnnouncementStatus.IsRejected = status.IsRejected;
            }
            else
            {
                announcement.AnnouncementStatus.IsApproved = status.IsApproved;
                announcement.AnnouncementStatus.IsChecked = status.IsChecked;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Pay() => View();

        [Authorize]
        public async Task<IActionResult> Check()
        {
            var userId = _userManager.GetUserAsync(User).Result.Id;
            var user = await _context.Users
                .Include(e => e.Purchase)
                .ThenInclude(e => e.Announcements)
                .FirstOrDefaultAsync(e => e.Id.Equals(userId));
            
            var account = user.Purchase.Announcements.Last();

            var check = new Check
            {
                CompanyName = "IL Piacere",
                Href = "ilpiacere.com",
                INN = "12342131",
                Number = account.Id,
                Outcome = Convert.ToInt32(account.Cost)
            };
            check.InitHeader();

            
            var checkProduct = new CheckProduct
            {
                Cost = Convert.ToInt32(account.Cost),
                Count = 1,
                Name = account.Name
            };
            check.AddProduct(checkProduct);
            
            return File(check.GetDocument(), "application/pdf");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AccountsSale.Models;
using AccountsSale.Models.Turnover;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using AccountsSale.Models.Users;
using Microsoft.AspNetCore.Authorization;
using AccountsSale.ViewModels;

namespace AccountsSale.Controllers
{
    [Authorize]
    public class AnnouncementsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        public AnnouncementsController(UserManager<User> userManager, ApplicationContext applicationContext)
        {
            _userManager = userManager;
            _context = applicationContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userSale = await _context.Sales
                .Include(x => x.Announcements)
                .Where(x => x.UserId.Equals(user.Id))
                .FirstOrDefaultAsync();

            return View(userSale);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AnnouncementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var sale = await _context.Sales
                    .Include(x => x.Announcements)
                    .Where(x => x.UserId.Equals(user.Id))
                    .FirstOrDefaultAsync();

                sale.Announcements.Add(new Announcement
                {
                    Name = model.Name,
                    Cost = Convert.ToDecimal(model.Cost),
                    Description = model.Description,
                    AccountPassword = model.AccountPassword,
                    AccountEmail = model.AccountLogin,
                    IsVip = model.IsVip,
                    CreationDate = DateTime.Now
                });

                await _context.SaveChangesAsync();

                if (model.IsVip)
                    return RedirectToAction("Pay", "AnnouncementProcessing");
            }
            else
            {
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var announcement = await _context
               .Announcements
               .FirstOrDefaultAsync(x => x.Id == id);

            var announcementViewModel = new AnnouncementViewModel
            {
                Id = announcement.Id,
                Cost = Convert.ToSingle(announcement.Cost),
                Description = announcement.Description,
                Name = announcement.Name
            };

            return View(announcementViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _context
               .Announcements
               .FirstOrDefaultAsync(x => x.Id == id);

            var announcementViewModel = new AnnouncementViewModel
            {
                Id = announcement.Id,
                Cost = Convert.ToSingle(announcement.Cost),
                Description = announcement.Description,
                Name = announcement.Name
            };

            return View(announcementViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AnnouncementViewModel model)
        {
            if (model.Name == null || model.Description == null)
                return View(model);

            var announcement = await _context
                .Announcements
                .FirstOrDefaultAsync(x => x.Id == model.Id);


            if (announcement != null)
            {
                announcement.Name = model.Name;
                announcement.Cost = Convert.ToDecimal(model.Cost);
                announcement.Description = model.Description;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var announcement = await _context
                .Announcements
                .FirstOrDefaultAsync(x => x.Id == id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}

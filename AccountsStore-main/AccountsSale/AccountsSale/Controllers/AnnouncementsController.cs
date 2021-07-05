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
        private readonly ApplicationContext _applicationContext;
        public AnnouncementsController(UserManager<User> userManager, ApplicationContext applicationContext)
        {
            _userManager = userManager;
            _applicationContext = applicationContext;
        }

        public async Task<IActionResult> Index()
        {
            var currentContextUser = await _userManager.GetUserAsync(User);
            var user = await _applicationContext
                .Users
                .Include(x => x.Sale)
                .ThenInclude(x => x.Announcements)
                .FirstOrDefaultAsync(x => x.Id == currentContextUser.Id);

            return View(user.Sale.Announcements.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnnouncementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var userSale = await _applicationContext.Sales.FirstOrDefaultAsync(x => x.UserId == user.Id);

                var announcement = new Announcement
                {
                    Name = model.Name,
                    Cost = Convert.ToDecimal(model.Cost),
                    Description = model.Description,
                    Sale = user.Sale
                };

                userSale.Announcements.Add(announcement);

                await _applicationContext.SaveChangesAsync();
            }


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var announcement = await _applicationContext
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
            var announcement = await _applicationContext
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var announcement = await _applicationContext
                .Announcements
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (announcement != null)
            {
                announcement.Name = model.Name;
                announcement.Cost = Convert.ToDecimal(model.Cost);
                announcement.Description = model.Description;

                await _applicationContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var announcement = await _applicationContext
                .Announcements
                .FirstOrDefaultAsync(x => x.Id == id);
            if (announcement != null)
            {
                _applicationContext.Announcements.Remove(announcement);
                await _applicationContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

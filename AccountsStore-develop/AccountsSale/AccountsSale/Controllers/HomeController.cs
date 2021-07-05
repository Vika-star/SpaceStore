using AccountsSale.Models;
using AccountsSale.Models.Turnover;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsSale.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly int _countAnnouncements = Constants.CountProductsOnHomePage;
        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var announcements = await _context.Announcements
                .Where(x => x.IsPaid == false 
                && x.AnnouncementStatus.IsApproved == true
                && x.AnnouncementStatus.IsChecked == true)
                .ToListAsync();

            var countProducts = announcements.Count();

            if (countProducts - _countAnnouncements <= 0)
                return View(new List<Announcement>());

            announcements = announcements.Skip(countProducts - _countAnnouncements)
                .Take(_countAnnouncements).ToList();

            announcements.Reverse();

            return View(announcements);
        }

        public IActionResult TradingArea()
        {
            return View();
        }

        public IActionResult Rules()
        {
            return View();
        }

        public IActionResult Contacts() => View();
        public IActionResult Assistance() => View();

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

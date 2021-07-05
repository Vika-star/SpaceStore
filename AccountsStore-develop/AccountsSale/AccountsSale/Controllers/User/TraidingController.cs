using AccountsSale.Models;
using AccountsSale.Models.Turnover;
using AccountsSale.Models.Turnover.ViewModels;
using AccountsSale.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsSale.Controllers
{
    public class TraidingController : Controller
    {
        private readonly int _pageSize = Constants.CountProductsOnPage;
        private readonly ApplicationContext _context;

        public TraidingController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(ProductsFavorProperties properties)
        {
            IQueryable<Announcement> products = _context.Announcements
                .Include(x => x.AnnouncementStatus)
                .Where(x=>x.AnnouncementStatus.IsChecked && !x.IsPaid);

            products = Filter(properties.CostFrom, 
                properties.CostTo, 
                properties.IsApprovedAnnouncements, 
                products);

            products = Sort(properties.sortOrder, products);

            var count = await products.CountAsync();
            var items = await products.Skip((properties.Page - 1) * _pageSize).Take(_pageSize).ToListAsync();

            var viewModel = new AnnouncementTraindingViewModel
            {
                PageViewModel = new PageViewModel(count, properties.Page, _pageSize),
                SortViewModel = new SortViewModel(properties.sortOrder),
                FilterViewModel = new FilterViewModel(properties.CostFrom, properties.CostTo, properties.IsApprovedAnnouncements),
                Products = items
            };
            return View(viewModel);
        }

        private IQueryable<Announcement> Sort(SortState sortOrder, IQueryable<Announcement> products) => sortOrder switch
        {
            SortState.NameAsc => products.OrderBy(s => s.Name),
            SortState.NameDesc => products.OrderByDescending(s => s.Name),
            SortState.CostAsc => products.OrderBy(s => s.Cost),
            SortState.CostDesc => products.OrderByDescending(s => s.Cost),
            _ => products,
        };

        private IQueryable<Announcement> Filter( int? costFrom, int? costTo, bool isApproved, IQueryable<Announcement> products)
        {
            if (isApproved)
                products = products.Where(x => x.AnnouncementStatus.IsApproved);

            if (costFrom != null && costTo != null)
                products = products.Where(x => x.Cost >= costFrom && x.Cost <= costTo);

            return products;
        }

        public async Task<IActionResult> Details(int id)
        {
            var announcement = await _context.Announcements
                .Include(x=>x.AnnouncementStatus)
                .FirstOrDefaultAsync(x=>x.Id.Equals(id));
            if (announcement == null)
                return NotFound();

            return View(announcement);
        }
    }
}
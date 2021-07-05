using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsSale.Models.Turnover.ViewModels
{
    public class FilterViewModel
    {
        public int SelectedCostForm { get; set; }
        public int SelectedCostTo { get; set; }
        public bool SelectedApprovedAnnouncements { get; set; }

        public FilterViewModel(int? selectedCostForm, int? selectedCostTo, bool isApprovedAnnouncements)
        {
            SelectedCostForm = selectedCostForm ?? 0;
            SelectedCostTo = selectedCostTo ?? int.MaxValue;
            SelectedApprovedAnnouncements = isApprovedAnnouncements;
        }
    }
}

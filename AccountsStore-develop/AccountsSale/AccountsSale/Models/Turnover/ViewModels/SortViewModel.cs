using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsSale.Models.Turnover.ViewModels
{
    public class SortViewModel
    {
        public SortState NameSort { get; private set; }
        public SortState CostSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            CostSort = sortOrder == SortState.CostAsc ? SortState.CostDesc : SortState.CostAsc;
            Current = sortOrder;
        }
    }
}

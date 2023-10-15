using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMan.Paging
{
    public class GridResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Items { get; set; }

        public GridResult(IReadOnlyList<T> items, int total)
        {
            Items = items;
            TotalCount = total;
        }
    }
}

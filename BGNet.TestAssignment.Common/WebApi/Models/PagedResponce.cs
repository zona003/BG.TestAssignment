
using System.Collections;

namespace BGNet.TestAssignment.Common.WebApi.Models
{
    public class PagedResponce<T>
    {
        public int Total { get; set; }
        public T Items { get; set; }

        public PagedResponce(int total, T items)
        {
            Total = total;
            Items = items;
        }

    }
}

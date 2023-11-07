
namespace BGNet.TestAssignment.Common.WebApi.Models
{
    public class PagedResponce<T>
    {
        public int Total { get; set; }
        public int PageSize { get; set; } = 10;
        public IQueryable<T> Items { get; set; }

        public PagedResponce(int total, IQueryable<T> items)
        {
            Total = total;
            Items = items;
        }

        public IQueryable ToPaged(int page)
        {
            return Items.Skip((page - 1) * PageSize).Take(PageSize);
        }
    }
}

namespace BGNet.TestAssignment.Common.WebApi.Models
{
    public class PagedResponse<T>
    {
        public int Total { get; set; }
        public T Items { get; set; }

        public PagedResponse(int total, T items)
        {
            Total = total;
            Items = items;
        }

    }
}
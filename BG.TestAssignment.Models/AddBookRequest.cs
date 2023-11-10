
namespace BGNet.TestAssignment.Models
{
    public class AddBookRequest
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? BookGenre { get; set; }

        public List<int>? AuthorsInBooks { get; set; }
    }
}

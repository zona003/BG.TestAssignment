namespace BGNet.TestAssignment.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? BookGenre { get; set; }
        public int AuthorId { get; set; }

        public IEnumerable<AuthorDTO>? Authors { get; set; }
    }
}

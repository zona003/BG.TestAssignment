namespace BGNet.TestAssignment.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? BookGenre { get; set; }
        public int AuthorId { get; set; }

        public IEnumerable<AuthorDto>? Authors { get; set; }
    }
}

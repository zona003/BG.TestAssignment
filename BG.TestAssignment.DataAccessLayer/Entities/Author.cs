namespace BGNet.TestAssignment.DataAccess.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public IEnumerable<Book>? Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}

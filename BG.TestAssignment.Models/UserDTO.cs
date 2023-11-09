namespace BGNet.TestAssignment.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
    }
}

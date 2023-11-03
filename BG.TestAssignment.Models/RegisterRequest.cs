namespace BGNet.TestAssignment.Models
{
    public class RegisterRequest
    {
        
        public required string UserName { get; set; }

        public required string Password { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }

    }
}

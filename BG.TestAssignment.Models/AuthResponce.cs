namespace BGNet.TestAssignment.Models
{
    public class AuthResponce
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public string Token { get; set; }
    }
}

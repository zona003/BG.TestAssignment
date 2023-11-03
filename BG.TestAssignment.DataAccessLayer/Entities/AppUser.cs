using Microsoft.AspNetCore.Identity;

namespace BGNet.TestAssignment.DataAccess.Entities
{
    public class AppUser : IdentityUser<long>
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpireTime { get; set; }
    }
}

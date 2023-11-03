using BGNet.TestAssignment.Models;

namespace BGNet.TestAssignment.Api.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> GetCurrentUser(string username);
    }
}

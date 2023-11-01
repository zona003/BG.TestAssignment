using BG.TestAssignment.Models;

namespace BG.TestAssignment.AuthApi.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> GetCurrentUser(string username);
    }
}

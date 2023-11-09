using BGNet.TestAssignment.Models;

namespace BGNet.TestAssignment.Api.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetCurrentUser(string username);
    }
}

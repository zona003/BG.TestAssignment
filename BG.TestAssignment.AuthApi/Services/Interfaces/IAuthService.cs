using BGNet.TestAssignment.Models;

namespace BGNet.TestAssignment.Api.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResponce> Login(AuthRequest request);

        public Task<bool> Register(RegisterRequest request);
    }
}

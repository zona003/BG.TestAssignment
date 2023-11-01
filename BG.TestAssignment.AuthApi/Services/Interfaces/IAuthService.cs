using BG.TestAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace BG.TestAssignment.AuthApi.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResponce> Login(AuthRequest request);

        public Task<bool> Register(RegisterRequest request);
    }
}

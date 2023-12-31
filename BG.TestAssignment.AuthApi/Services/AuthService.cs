﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BG.TestAssignment.DataAccess;
using BGNet.TestAssignment.Api.Services.Interfaces;
using BGNet.TestAssignment.Business.Validators;
using BGNet.TestAssignment.Common.WebApi.Models;
using BGNet.TestAssignment.DataAccess;
using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace BGNet.TestAssignment.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BookAuthorsDataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(BookAuthorsDataContext context, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponce> Login(AuthRequest request)
        {
            if (request == null)
            {
                return new AuthResponce();
            }

            LoginValidator validator = new();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return new AuthResponce();
            }

            var managedUser = await _userManager.FindByNameAsync(request.UserName);

            if (managedUser == null)
            {
                return new AuthResponce();
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

            if (!isPasswordValid)
            {
                return new AuthResponce();
            }

            var userRoles = await _userManager.GetRolesAsync(managedUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  managedUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(userRoles.Select(claim => new Claim(ClaimTypes.Name, claim)));

            string token = GenerateToken(claims);

            AuthResponce authResponce = managedUser.Adapt<AuthResponce>();
            authResponce.Token = token;
            
            return authResponce;

        }

        public async Task<bool> Register(RegisterRequest request)
        {
            if (request == null)
            {
                return false;
            }

            RegisterValidator validator = new();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return false;
            }

            var userExist = await _userManager.FindByNameAsync(request.UserName);
            if (userExist != null)
                return false;

            AppUser user = request.Adapt<AppUser>();

            var createUserResult = await _userManager.CreateAsync(user, request.Password);
            if (!createUserResult.Succeeded)
                throw new Exception($"User {request.UserName} not {createUserResult.Errors}");



            var findUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);



            return findUser == null ? throw new Exception($"User {request.UserName} not found") : true;
        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            JWTOptions? jwt = _configuration.GetSection(JWTOptions.Jwt).Get<JWTOptions>();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwt.Issuer,
                Audience = jwt.Audience,
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

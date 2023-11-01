using BG.TestAssignment.Business.BusinessLogic;
using BG.TestAssignment.Business.BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BG.TestAssignment.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace BG.TestAssignment.BooksAuthors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddScoped<IMapper, ServiceMapper>();

            builder.Services.AddScoped<IAuthorService, AuthorsServices>();
            builder.Services.AddScoped<IBooksService, BooksServices>();

            builder.Services.AddDbContext<BookAuthorsDataContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultPostgreDB")));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(build =>
                {
                    build.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

            });

            builder.Services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"]!,
                        ValidAudience = builder.Configuration["Jwt:Audience"]!,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
                    };
                });

            builder.Services.AddAuthorization(opt =>
                opt.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.MapControllers();

            app.Run();
        }
    }
}
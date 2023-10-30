using BG.TestAssignment.Business.BusinessLogic;
using Microsoft.EntityFrameworkCore;
using BG.TestAssignment.DataAccessLayer.DataContext;


namespace BG.TestAssignment.BooksAuthors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddScoped<IMapper, ServiceMapper>();

            //builder.Services.AddScoped<AuthorsBL, AuthorsBL>();
            //builder.Services.AddScoped<BooksBL, BooksBL>();

            builder.Services.AddDbContext<BookAuthorsDataContext>(options =>
                options.UseNpgsql("Host = localhost; Port = 5432; Database = booksdb; Username = postgres; Password = root"));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(build =>
                {
                    build.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

            });

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

            app.UseAuthorization();

            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}
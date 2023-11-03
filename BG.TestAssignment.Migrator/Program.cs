﻿using BG.TestAssignment.DataAccess;
using BG.TestAssignment.DataAccess.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BG.TestAssignment.Migrator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args).Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BookAuthorsDataContext>();
                context.Database.Migrate();
            }
            host.Start();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Program>();

    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.Models
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product() { Name = "Product 1", Description = "Product Description 1", Price = 100, Category = "Category 1"},
                    new Product() { Name = "Product 2", Description = "Product Description 2", Price = 200, Category = "Category 1"},
                    new Product() { Name = "Product 3", Description = "Product Description 3", Price = 300, Category = "Category 1"},
                    new Product() { Name = "Product 4", Description = "Product Description 4", Price = 400, Category = "Category 1"},
                    new Product() { Name = "Product 5", Description = "Product Description 5", Price = 500, Category = "Category 1"},
                    new Product() { Name = "Product 6", Description = "Product Description 6", Price = 600, Category = "Category 1"}
                    );

                context.SaveChanges();
            }
        }
    }
}

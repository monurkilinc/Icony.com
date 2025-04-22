using Icony.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icony.Data.Seed
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager=scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Migration calistirma
            context.Database.EnsureCreated();

            // Rollerin olusturulmasi
            if(!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            //Admin kullanicisi yoksa olustur.
            if(await userManager.FindByEmailAsync("admin@icony.com") == null)
            {
                var admin = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@icony.com",
                    FullName = "SuperAdmin",
                };

                await userManager.CreateAsync(admin,"Admin123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Kategorilerin eklenmesi
            if (!context.Categories.Any())
            {
                var c1 = new Category { Name = "Pantolon" };
                var c2 = new Category { Name = "Gomlek" };
                var c3 = new Category { Name = "Tshirt" };
                var c4 = new Category { Name = "Corap" };
                var c5 = new Category { Name = "Ayakkabi" };
                
                context.Categories.AddRange(c1,c2,c3,c4,c5);
                context.SaveChanges();

                // Urunleri ekle
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kargo Pantolon",
                        Description = "Slim fit haki kargo pantolon",
                        Price = 349.50m,
                        Stock = 50,
                        ImageUrl = "/img/product/product-1.jpg",
                        CategoryId = c1.Id
                    },
                     new Product
                     {
                         Name = "Jean Gomlek",
                         Description = "Regular kot gomlek",
                         Price = 349.50m,
                         Stock = 50,
                         ImageUrl = "/img/product/product-2.jpg",
                         CategoryId = c2.Id
                     },
                    new Product
                    {
                        Name = "Oversize Tişört",
                        Description = "Pamuklu siyah tişört",
                        Price = 199.99m,
                        Stock = 100,
                        ImageUrl = "/img/product/product-3.jpg",
                        CategoryId = c3.Id
                    },
                    new Product
                    {
                        Name = "Beyaz Corap",
                        Description = "Pamuklu beyaz corap",
                        Price = 199.99m,
                        Stock = 100,
                        ImageUrl = "/img/product/product-4.jpg",
                        CategoryId = c4.Id
                    },
                    new Product
                    {
                        Name = "Spor Ayakkabi",
                        Description = "Beyaz tabanlı sneaker",
                        Price = 499.99m,
                        Stock = 70,
                        ImageUrl = "/img/product/product-5.jpg",
                        CategoryId = c5.Id
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

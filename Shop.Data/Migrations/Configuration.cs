namespace Shop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Shop.Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Shop.Data.TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Shop.Data.TeduShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TeduShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "tedu",
                Email = "abc@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Phuc Thien Pham"
            };
            manager.Create(user, "123456");

            if (!rolemanager.Roles.Any())
            {
                rolemanager.Create(new IdentityRole { Name = "Admin" });
                rolemanager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail(user.Email);
            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });

            //
            CreateProductCategorySample(context);
        }

        protected void CreateProductCategorySample(Shop.Data.TeduShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> productCategories = new List<ProductCategory>()
            {
                new ProductCategory()
                {
                    Name="Product cat 1",
                    Alias="Alias product cat 1",
                    Status = true
                },
                new ProductCategory()
                {
                    Name="Product cat 2",
                    Alias="Alias product cat 2",
                    Status = true
                },
                new ProductCategory()
                {
                    Name="Product cat 3",
                    Alias="Alias product cat 3",
                    Status = true
                }
            };

                context.ProductCategories.AddRange(productCategories);
                context.SaveChanges();
            }
        }
    }
}

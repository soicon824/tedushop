namespace Shop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Shop.Common;
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
            CreateFooter(context);
            CreateSlide(context);
            CreatePage(context);
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

        private void CreateFooter(TeduShopDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "";
            }
        }

        private void CreateSlide(TeduShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>();
                listSlide.Add(new Slide()
                {
                    Name = "bag",
                    Image = "/Assets/client/images/bag.jpg",
                    DisplayOrder = 0,
                    Status = true,
                    Url = "#",
                    Content = @"
                    <h2>FLAT 50% 0FF 1</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                                <span class=""on - get"">GET NOW</span>"
                });
                listSlide.Add(new Slide()
                {
                    Name = "bag1",
                    Image = "/Assets/client/images/bag1.jpg",
                    DisplayOrder = 1,
                    Status = true,
                    Url = "#",
                    Content = @"
                    <h2>FLAT 50% 0FF 2</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                                <span class=""on - get"">GET NOW</span>"
                });
                listSlide.Add(new Slide()
                {
                    Name = "bag3",
                    Image = "/Assets/client/images/bag.jpg",
                    DisplayOrder = 2,
                    Status = true,
                    Url = "#",
                    Content = @"
                    <h2>FLAT 50% 0FF 3</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                                <span class=""on - get"">GET NOW</span>"
                });
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }
        private void CreatePage(TeduShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                List<Page> listPages = new List<Page>();
                listPages.Add(new Page()
                {
                    Status = true,
                    Name = "gioithieu",
                    Alias = "gioi-thieu",
                    Content = @"
                    Lorem ipsum dolor sit amet, 
                    consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. 
                    Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper 
                    suscipit lobortis nisl ut aliquip ex ea commodo consequat. 
                    Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse 
                    molestie consequat, vel illum dolore eu feugiat nulla facilisis at 
                    vero eros et accumsan et iusto odio dignissim qui blandit praesent 
                    luptatum zzril delenit augue duis dolore te feugait nulla facilisi. 
                    Nam liber tempor cum soluta nobis eleifend option congue nihil 
                    imperdiet doming id quod mazim placerat facer possim assum."
                });
                context.Pages.AddRange(listPages);
                context.SaveChanges();
            }
        }
    }
}

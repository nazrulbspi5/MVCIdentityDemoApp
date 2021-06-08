using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCIdentityDemoApp.Models;

namespace MVCIdentityDemoApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCIdentityDemoApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    dbContext.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            AddRoles(context);
            AddUser(context);
        }

        private void AddUser(ApplicationDbContext dbcontext)
        {
            string email = "nazrulbspi5@gmail.com";
            ApplicationUser applicationUser = dbcontext.Users.FirstOrDefault(x => x.Email == email);
            if (applicationUser == null)
            {
                var userId = Guid.NewGuid().ToString();
                IdentityRole identityRole = dbcontext.Roles.First(x => x.Name == "Admin");
                IdentityUserRole userRole=new IdentityUserRole()
                {
                    UserId = userId,RoleId = identityRole.Id
                };
                PasswordHasher hasher=new PasswordHasher();
                string password = hasher.HashPassword("123456");
                applicationUser = new ApplicationUser()
                {
                    Email = email,UserName = email,PasswordHash = password,Roles = { userRole}
                };
                dbcontext.Users.Add(applicationUser);
                dbcontext.SaveChanges();
            }
        }

        private void AddRoles(ApplicationDbContext dbContext)
        {
            List<string>roles=new List<string>() {"Admin", "User"};
            foreach (string role in roles)
            {
              IdentityRole identityRole=  dbContext.Roles.FirstOrDefault(x => x.Name == role);
              if (identityRole==null)
              {
                  identityRole =new IdentityRole(role);
                  dbContext.Roles.Add(identityRole);
              }
            }

            dbContext.SaveChanges();
        }
    }
}

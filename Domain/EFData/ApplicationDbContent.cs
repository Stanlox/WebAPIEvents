using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EFData
{
    public class ApplicationDbContent : IdentityDbContext<User>
    {
        public ApplicationDbContent(DbContextOptions<ApplicationDbContent> options)
            : base(options)
        {
        }

        public DbSet<Event> Event { get; set; }

        public static async Task CreateAdminAccount(IConfiguration config, IServiceScope scope)
        {
            UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string username = config["Data:AdminUser:Name"];
            string email = config["Data:AdminUser:Email"];
            string password = config["Data:AdminUser:Password"];
            string role = config["Data:AdminUser:Role"];

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                User user = new User
                {
                    UserName = username,
                    Email = email,
                    role = role
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}

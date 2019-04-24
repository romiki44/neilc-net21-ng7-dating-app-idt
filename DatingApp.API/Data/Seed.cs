using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles=new List<Role> {
                    new Role{Name="Member"},
                    new Role{Name="Admin"},
                    new Role{Name="Moderator"},
                    new Role{Name="VIP"}
                };

                foreach(var role in roles) {
                    await _roleManager.CreateAsync(role);
                }

                foreach (var user in users) {
                    user.Photos.SingleOrDefault().IsApproved=true;
                    await _userManager.CreateAsync(user, "password");
                    await _userManager.AddToRoleAsync(user, "Member");
                }

                var adminUser=new User {
                    UserName="Admin"
                };

                IdentityResult result=await _userManager.CreateAsync(adminUser, "password");

                if(result.Succeeded) {
                    var admin=await _userManager.FindByNameAsync("Admin");
                    await _userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});
                }
            }
        }
    }
}
using ConferenceRoom.Helpers;
using Microsoft.AspNetCore.Identity;

namespace ConferenceRoom.Seed
{
    public class UserSeed
    {
        public static async Task SeedAdmin(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var adminEmail = "admin@conferenceroom.com";
                //check if admin@conferenceroom.com exists
                var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
                if (existingAdmin == null)
                {
                    ///krijo
                    //email => admin@conferenceroom.com, ConfirmedEmail = true, password = Admin123*
                    var newAdmin = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };
                    var result = await userManager.CreateAsync(newAdmin, "Admin123*");
                    //assign role Constants.AdminRole
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newAdmin, Constants.AdminRole);
                    }
                }
            }
        }
    }
}
 

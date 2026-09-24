using Microsoft.AspNetCore.Identity;

namespace JobApplication.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync( RoleManager<IdentityRole> roleManager)
        {
            string[] roles =
            {
                "Recruiter",
                "Candidate"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }
        }
    }
}
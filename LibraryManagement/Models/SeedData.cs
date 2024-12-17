using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task InitializeRoles(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        // List of roles to be created
        string[] roleNames = { "Admin", "User" };

        // Ensure each role is created if it doesn't exist
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Admin user details
        var adminEmail = "admin@gmail.com"; // Replace with desired admin email
        var adminPassword = "Admin@123"; // Replace with desired admin password

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            // If the admin user doesn't exist, create it
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Administrator", // Optional: set custom fields like FullName
                PhoneNumber = "123-456-7890" // Provide a default phone number
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                // Assign Admin role to the user
                var addToRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!addToRoleResult.Succeeded)
                {
                    Console.WriteLine($"Error adding admin role to user: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                // If user creation failed, handle the error appropriately
                Console.WriteLine($"Error creating admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            // If the admin user exists, update any missing or invalid fields
            bool needsUpdate = false;
            if (string.IsNullOrEmpty(adminUser.Email))
            {
                adminUser.Email = adminEmail; // Ensure email is set
                needsUpdate = true;
            }
            if (string.IsNullOrEmpty(adminUser.UserName))
            {
                adminUser.UserName = adminEmail; // Ensure username is set
                needsUpdate = true;
            }
            if (string.IsNullOrEmpty(adminUser.PhoneNumber))
            {
                adminUser.PhoneNumber = "123-456-7890"; // Provide a default phone number
                needsUpdate = true;
            }

            if (needsUpdate)
            {
                var result = await userManager.UpdateAsync(adminUser);
                if (!result.Succeeded)
                {
                    Console.WriteLine($"Error updating admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }

}

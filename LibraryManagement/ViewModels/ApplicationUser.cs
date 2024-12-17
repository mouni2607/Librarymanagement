using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; } // Ensure this exists and is used properly
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class ContactMessage
    {
        public int ContactMessageId { get; set; } // Primary key

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }  // Store phone as string to handle different formats

        
    }
}

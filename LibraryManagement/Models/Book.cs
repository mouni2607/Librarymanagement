using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class Book
    {
        [BindNever]
        public int BookId { get; set; } // Primary key, not bound from user input

        [Required(ErrorMessage = "The Title field is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Author field is required.")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "The ISBN field is required.")]
        [RegularExpression(@"^\d{3}-\d{10}$", ErrorMessage = "ISBN must be in the format XXX-XXXXXXXXXX.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "The Published Date field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        [BindNever]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; } = true; // Default to available

       
   
      
        // Navigation Property for Borrow Records
        [BindNever]
        public ICollection<BorrowRecord>? BorrowRecords { get; set; }
        // New property to hold the image path
        [StringLength(255, ErrorMessage = "Image path cannot exceed 255 characters.")]
        public string? ImagePath { get; set; } // Store the relative path to the image

    }
}

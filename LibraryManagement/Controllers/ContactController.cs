using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Authorize] // Ensures that the controller can only be accessed by authenticated users
    public class ContactController : Controller
    {
        private readonly LibraryContext _context;

        public ContactController(LibraryContext context)
        {
            _context = context;
        }

        // GET: /Contact
        public IActionResult Index()
        {
            // If you want to display data from database, you can fetch the first contact message or predefined values
            var contactInfo = new ContactMessage
            {
                Email = "admin@gmail.com",  // You can fetch this from your database
                PhoneNumber = "+1234567890" // Same as above
            };

            // Passing the contact info to the view
            return View(contactInfo);
        }
    }
}

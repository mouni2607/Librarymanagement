using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Controllers
{
    public class BorrowController : Controller
    {
        private readonly LibraryContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        // Constructor to inject LibraryContext and UserManager
        public BorrowController(LibraryContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Displays the borrow form for a specific book.
        // GET: Borrow/Create/5
        public async Task<IActionResult> Create(int? bookId)
        {
            if (bookId == null || bookId == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided for borrowing.";
                return View("NotFound");
            }

            try
            {
                var book = await _context.Books.FindAsync(bookId);
                if (book == null)
                {
                    TempData["ErrorMessage"] = $"No book found with ID {bookId} to borrow.";
                    return View("NotFound");
                }

                if (!book.IsAvailable)
                {
                    TempData["ErrorMessage"] = $"The book '{book.Title}' is currently not available for borrowing.";
                    return View("NotAvailable");
                }

                // Get the current logged-in user
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    TempData["ErrorMessage"] = "You must be logged in to borrow a book.";
                    return RedirectToAction("Login", "Account");
                }

                // Fetch user's profile information (name, email, and phone)
                var borrowViewModel = new BorrowViewModel
                {
                    BookId = book.BookId,
                    BookTitle = book.Title,
                    BorrowerName = user.FullName,  // Use FullName from ApplicationUser or whatever property your User model has
                    BorrowerEmail = user.Email,    // Auto-fill the email from the logged-in user
                    Phone = user.PhoneNumber       // Auto-fill the phone number from the logged-in user
                };

                return View(borrowViewModel);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the borrow form.";
                return View("Error");
            }
        }

        // Processes the borrowing action, creates a BorrowRecord, updates the book's availability
        // POST: Borrow/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BorrowViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var book = await _context.Books.FindAsync(model.BookId);
                if (book == null)
                {
                    TempData["ErrorMessage"] = $"No book found with ID {model.BookId} to borrow.";
                    return View("NotFound");
                }

                if (!book.IsAvailable)
                {
                    TempData["ErrorMessage"] = $"The book '{book.Title}' is already borrowed.";
                    return View("NotAvailable");
                }

                // Get the current logged-in user
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    TempData["ErrorMessage"] = "You must be logged in to borrow a book.";
                    return RedirectToAction("Login", "Account");
                }

                // Ensure the email provided matches the logged-in user's email
                if (model.BorrowerEmail != user.Email)
                {
                    TempData["ErrorMessage"] = "The email provided does not match your registered email.";
                    return View(model);
                }

                var borrowRecord = new BorrowRecord
                {
                    BookId = book.BookId,
                    BorrowerName = model.BorrowerName,
                    BorrowerEmail = model.BorrowerEmail,
                    Phone = model.Phone,
                    BorrowDate = DateTime.UtcNow // Set the current date as the borrow date
                };

                // Update the book's availability
                book.IsAvailable = false;

                _context.BorrowRecords.Add(borrowRecord);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Successfully borrowed the book: {book.Title}.";
                return RedirectToAction("Index", "Books");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while processing the borrowing action.";
                return View("Error");
            }
        }

        public async Task<IActionResult> History(string searchTerm)
        {
            try
            {
                // Get the current logged-in user
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    TempData["ErrorMessage"] = "You must be logged in to view your borrowing history.";
                    return RedirectToAction("Login", "Account");
                }

                // Check if the user is an admin
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

                // Pass the isAdmin value to the view
                ViewData["IsAdmin"] = isAdmin;
                ViewData["SearchTerm"] = searchTerm; // Add search term to view for maintaining state in the search box

                // Existing logic to fetch borrow records
                IQueryable<BorrowRecord> borrowRecordsQuery = _context.BorrowRecords.Include(br => br.Book)
                    .OrderByDescending(br => br.BorrowDate);

                if (!isAdmin)
                {
                    // If the user is not an admin, show only their own borrow records
                    borrowRecordsQuery = borrowRecordsQuery.Where(br => br.BorrowerEmail == user.Email);
                }

                // Filter by searchTerm if provided
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    borrowRecordsQuery = borrowRecordsQuery.Where(br =>
                        br.Book.Title.Contains(searchTerm) ||
                        br.BorrowerName.Contains(searchTerm) ||
                        br.BorrowerEmail.Contains(searchTerm));
                }

                var borrowRecords = await borrowRecordsQuery.ToListAsync();

                return View(borrowRecords);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the borrowing history.";
                return View("Error");
            }
        }


        // Displays the return confirmation for a specific borrow record
        // GET: Borrow/Return/5
        public async Task<IActionResult> Return(int? borrowRecordId)
        {
            if (borrowRecordId == null || borrowRecordId == 0)
            {
                TempData["ErrorMessage"] = "Borrow Record ID was not provided for returning.";
                return View("NotFound");
            }

            try
            {
                var borrowRecord = await _context.BorrowRecords
                    .Include(br => br.Book)
                    .FirstOrDefaultAsync(br => br.BorrowRecordId == borrowRecordId);

                if (borrowRecord == null)
                {
                    TempData["ErrorMessage"] = $"No borrow record found with ID {borrowRecordId} to return.";
                    return View("NotFound");
                }

                if (borrowRecord.ReturnDate != null)
                {
                    TempData["ErrorMessage"] = $"The borrow record for '{borrowRecord.Book.Title}' has already been returned.";
                    return View("AlreadyReturned");
                }

                // Get the current logged-in user
                var user = await _userManager.GetUserAsync(User);

                if (user == null || borrowRecord.BorrowerEmail != user.Email)
                {
                    TempData["ErrorMessage"] = "You are not authorized to return this book.";
                    return RedirectToAction("History");
                }

                var returnViewModel = new ReturnViewModel
                {
                    BorrowRecordId = borrowRecord.BorrowRecordId,
                    BookTitle = borrowRecord.Book.Title,
                    BorrowerName = borrowRecord.BorrowerName,
                    BorrowDate = borrowRecord.BorrowDate
                };

                return View(returnViewModel);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the return confirmation.";
                return View("Error");
            }
        }

        // Processes the return action, updates the BorrowRecord with the return date, updates the book's availability
        // POST: Borrow/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(ReturnViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var borrowRecord = await _context.BorrowRecords
                    .Include(br => br.Book)
                    .FirstOrDefaultAsync(br => br.BorrowRecordId == model.BorrowRecordId);

                if (borrowRecord == null)
                {
                    TempData["ErrorMessage"] = $"No borrow record found with ID {model.BorrowRecordId} to return.";
                    return View("NotFound");
                }

                if (borrowRecord.ReturnDate != null)
                {
                    TempData["ErrorMessage"] = $"The borrow record for '{borrowRecord.Book.Title}' has already been returned.";
                    return View("AlreadyReturned");
                }

                // Update the borrow record with the return date
                borrowRecord.ReturnDate = DateTime.UtcNow;

                // Update the book's availability
                borrowRecord.Book.IsAvailable = true;
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Successfully returned the book: {borrowRecord.Book.Title}.";
                return RedirectToAction("History");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while processing the return action.";
                return View("Error");
            }
        }
    }
}

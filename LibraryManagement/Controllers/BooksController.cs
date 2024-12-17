using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        // Injecting the LibraryContext and Logger to interact with the database and log events.
        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(string searchTerm)
        {
            var booksQuery = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Title.Contains(searchTerm) ||
                    b.Author.Contains(searchTerm) ||
                    b.ISBN.Contains(searchTerm));
            }

            var books = await booksQuery
                .Include(b => b.BorrowRecords)
                .ToListAsync();

            return View(books);
        }

        // GET: Books/Details/5
      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided.";
                return View("NotFound");
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                TempData["ErrorMessage"] = $"No book found with ID {id}.";
                return View("NotFound");
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Book book, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Handle image upload if a file is provided
                    if (image != null && image.Length > 0)
                    {
                        // Define upload directory
                        var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                        if (!Directory.Exists(uploadDir))
                        {
                            Directory.CreateDirectory(uploadDir);
                        }

                        // Create a unique file name to avoid overwriting
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(uploadDir, fileName);

                        // Save the image to the server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        // Set the relative path for storing in the database
                        book.ImagePath = "/images/" + fileName;
                    }

                    // Add the new book to the database
                    _context.Books.Add(book);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"Successfully added the book: {book.Title}.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while adding the book.";
                    return View(book);
                }
            }
            return View(book);
        }


        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided for editing.";
                return View("NotFound");
            }

            var book = await _context.Books.AsNoTracking()
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                TempData["ErrorMessage"] = $"No book found with ID {id} for editing.";
                return View("NotFound");
            }

            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, Book book, IFormFile? image)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided for updating.";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBook = await _context.Books.FindAsync(id);

                    if (existingBook == null)
                    {
                        TempData["ErrorMessage"] = $"No book found with ID {id} for updating.";
                        return View("NotFound");
                    }

                    // Handle image upload if a new image is uploaded
                    if (image != null && image.Length > 0)
                    {
                        // Delete the old image if it exists
                        if (!string.IsNullOrEmpty(existingBook.ImagePath))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingBook.ImagePath.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Upload new image
                        var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                        if (!Directory.Exists(uploadDir))
                        {
                            Directory.CreateDirectory(uploadDir);
                        }

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(uploadDir, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        // Update the ImagePath
                        existingBook.ImagePath = "/images/" + fileName;
                    }

                    // Update other properties
                    existingBook.Title = book.Title;
                    existingBook.Author = book.Author;
                    existingBook.ISBN = book.ISBN;
                    existingBook.PublishedDate = book.PublishedDate;

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"Successfully updated the book: {book.Title}.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the book.";
                    return View("Error");
                }
            }

            return View(book);
        }
    


// GET: Books/Delete/5
[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided for deletion.";
                return View("NotFound");
            }

            var book = await _context.Books.AsNoTracking()
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                TempData["ErrorMessage"] = $"No book found with ID {id} for deletion.";
                return View("NotFound");
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                TempData["ErrorMessage"] = $"No book found with ID {id} for deletion.";
                return View("NotFound");
            }

            if (!string.IsNullOrEmpty(book.ImagePath))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Successfully deleted the book: {book.Title}.";
            return RedirectToAction(nameof(Index));
        }
    }


   
}
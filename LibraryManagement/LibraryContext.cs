using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models
{
    public class LibraryContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        // Seed initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                // Existing books
                new Book
                {
                    BookId = 1,
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt and David Thomas",
                    ISBN = "978-0201616224",
                    PublishedDate = new DateTime(2021, 10, 30),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 2,
                    Title = "Design Pattern using C#",
                    Author = "Robert C. Martin",
                    ISBN = "978-0132350884",
                    PublishedDate = new DateTime(2023, 8, 1),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 3,
                    Title = "Mastering ASP.NET Core",
                    Author = "Pranaya Kumar Rout",
                    ISBN = "978-0451616235",
                    PublishedDate = new DateTime(2022, 11, 22),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 4,
                    Title = "SQL Server with DBA",
                    Author = "Rakesh Kumar",
                    ISBN = "978-4562350123",
                    PublishedDate = new DateTime(2020, 8, 15),
                    IsAvailable = true,
                    ImagePath = ""
                },
                // New Books
                new Book
                {
                    BookId = 5,
                    Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                    Author = "Robert C. Martin",
                    ISBN = "978-0132350884",
                    PublishedDate = new DateTime(2008, 8, 11),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 6,
                    Title = "The Clean Coder: A Code of Conduct for Professional Programmers",
                    Author = "Robert C. Martin",
                    ISBN = "978-0137081073",
                    PublishedDate = new DateTime(2011, 5, 1),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 7,
                    Title = "Refactoring: Improving the Design of Existing Code",
                    Author = "Martin Fowler",
                    ISBN = "978-0201485677",
                    PublishedDate = new DateTime(1999, 6, 8),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 8,
                    Title = "Design Patterns: Elements of Reusable Object-Oriented Software",
                    Author = "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides",
                    ISBN = "978-0201633610",
                    PublishedDate = new DateTime(1994, 11, 15),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 9,
                    Title = "Learning SQL",
                    Author = "Alan Beaulieu",
                    ISBN = "978-0596520830",
                    PublishedDate = new DateTime(2009, 5, 22),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 10,
                    Title = "Introduction to Algorithms",
                    Author = "Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest, Clifford Stein",
                    ISBN = "978-0262033848",
                    PublishedDate = new DateTime(2009, 7, 31),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 11,
                    Title = "The Art of Computer Programming",
                    Author = "Donald E. Knuth",
                    ISBN = "978-0321751041",
                    PublishedDate = new DateTime(2011, 6, 15),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 12,
                    Title = "Java: The Complete Reference",
                    Author = "Herbert Schildt",
                    ISBN = "978-0071808552",
                    PublishedDate = new DateTime(2014, 6, 4),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 13,
                    Title = "C# in Depth",
                    Author = "Jon Skeet",
                    ISBN = "978-1617291345",
                    PublishedDate = new DateTime(2019, 3, 15),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 14,
                    Title = "Python Crash Course",
                    Author = "Eric Matthes",
                    ISBN = "978-1593279288",
                    PublishedDate = new DateTime(2019, 11, 4),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 15,
                    Title = "JavaScript: The Good Parts",
                    Author = "Douglas Crockford",
                    ISBN = "978-0596517748",
                    PublishedDate = new DateTime(2008, 5, 15),
                    IsAvailable = true
                },
                new Book
                {
                    BookId = 16,
                    Title = "The Mythical Man-Month",
                    Author = "Frederick P. Brooks Jr.",
                    ISBN = "978-0201835953",
                    PublishedDate = new DateTime(1995, 11, 15),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 17,
                    Title = "Operating System Concepts",
                    Author = "Abraham Silberschatz, Peter B. Galvin, Greg Gagne",
                    ISBN = "978-1118063330",
                    PublishedDate = new DateTime(2013, 3, 25),
                    IsAvailable = true,
                    ImagePath = ""
                },
                new Book
                {
                    BookId = 18,
                    Title = "Code Complete",
                    Author = "Steve McConnell",
                    ISBN = "978-0735619678",
                    PublishedDate = new DateTime(2004, 6, 9),
                    IsAvailable = true,
                    ImagePath = ""
                }
            );
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        
    }
}

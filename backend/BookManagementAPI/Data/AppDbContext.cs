using Microsoft.EntityFrameworkCore;
using backend.Models;  // Import the Book model namespace

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor to pass DbContextOptions to the base class (DbContext)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet represents the collection of books in the database
        public DbSet<Book> Books { get; set; }
    }
}

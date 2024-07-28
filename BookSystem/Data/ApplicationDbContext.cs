using Microsoft.EntityFrameworkCore;
using BookSystem.Models;

namespace BookSystem.Data
{
    public class ApplicationDbContext : DbContext // Ensure the class is public
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}

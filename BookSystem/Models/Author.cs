namespace BookSystem.Models
{
    public class Author
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Biography { get; set; }

        // Navigation property to represent the one-to-many relationship
        public ICollection<Book?> Books { get; set; }
    }
}

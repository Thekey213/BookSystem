namespace BookSystem.Models
{
    public class Book
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public int? PublishedYear { get; set; }

        // Foreign key
        public int? AuthorId {get; set; }
        
        // Navigation property to represent the many-to-one relationship
        public Author? Author { get; set; }
    }
}

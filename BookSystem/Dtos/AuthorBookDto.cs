namespace BookSystem.Dtos
{
    public class AuthorBookDto
{
    public string? AuthorName { get; set; }
    public List<string> BookTitles { get; set; } = new List<string>();
}

}

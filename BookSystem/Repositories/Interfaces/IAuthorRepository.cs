using BookSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSystem.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
        Task<bool> AuthorExistsAsync(int id);
        Task<IEnumerable<Author>> GetAuthorsWithBooksAsync();
    }
}

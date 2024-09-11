using BookSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSystem.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
        Task<IEnumerable<Author>> GetAuthorsWithBooksAsync();
    }
}

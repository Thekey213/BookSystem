using BookSystem.Models;
using BookSystem.Repositories.Interfaces;
using BookSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSystem.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAuthorsAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _authorRepository.GetAuthorByIdAsync(id);
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _authorRepository.AddAuthorAsync(author);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            if (!await _authorRepository.AuthorExistsAsync(author.Id))
            {
                throw new KeyNotFoundException("Author not found.");
            }

            await _authorRepository.UpdateAuthorAsync(author);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            if (!await _authorRepository.AuthorExistsAsync(id))
            {
                throw new KeyNotFoundException("Author not found.");
            }

            await _authorRepository.DeleteAuthorAsync(id);
        }

        public async Task<IEnumerable<Author>> GetAuthorsWithBooksAsync()
        {
            return await _authorRepository.GetAuthorsWithBooksAsync();
        }
    }
}

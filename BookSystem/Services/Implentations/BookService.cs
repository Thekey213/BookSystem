using BookSystem.Models;
using BookSystem.Repositories.Interfaces;
using BookSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSystem.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            await _bookRepository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            if (!await _bookRepository.BookExistsAsync(book.Id ?? 0))
            {
                throw new KeyNotFoundException("Book not found.");
            }

            await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            if (!await _bookRepository.BookExistsAsync(id))
            {
                throw new KeyNotFoundException("Book not found.");
            }

            await _bookRepository.DeleteBookAsync(id);
        }

        public async Task<bool> BookExistsAsync(int id)
        {
            return await _bookRepository.BookExistsAsync(id);
        }
    }
}

using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookSystem.Models;
using BookSystem.Repositories.Interfaces;
using BookSystem.Services.Interfaces;
using BookSystem.Services.Implementations;
using System;

namespace BookSystem.Tests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly IBookService _bookService;

        public BookServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _bookService = new BookService(_mockBookRepository.Object);
        }

        [Fact]
        public async Task GetAllBooksAsync_ReturnsListOfBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1" },
                new Book { Id = 2, Title = "Book 2" }
            };

            _mockBookRepository.Setup(repo => repo.GetAllBooksAsync())
                .ReturnsAsync(books);

            // Act
            var result = await _bookService.GetAllBooksAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, b => b.Title == "Book 1");
            Assert.Contains(result, b => b.Title == "Book 2");
        }

        [Fact]
        public async Task GetBookByIdAsync_ReturnsBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book 1" };

            _mockBookRepository.Setup(repo => repo.GetBookByIdAsync(1))
                .ReturnsAsync(book);

            // Act
            var result = await _bookService.GetBookByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Book 1", result.Title);
        }

        [Fact]
        public async Task AddBookAsync_CallsRepositoryAddBookAsync()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "New Book" };

            _mockBookRepository.Setup(repo => repo.AddBookAsync(book))
                .Returns(Task.CompletedTask);

            // Act
            await _bookService.AddBookAsync(book);

            // Assert
            _mockBookRepository.Verify(repo => repo.AddBookAsync(book), Times.Once);
        }

        [Fact]
        public async Task UpdateBookAsync_BookExists_UpdatesBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Updated Book" };

            _mockBookRepository.Setup(repo => repo.BookExistsAsync(1))
                .ReturnsAsync(true);

            _mockBookRepository.Setup(repo => repo.UpdateBookAsync(book))
                .Returns(Task.CompletedTask);

            // Act
            await _bookService.UpdateBookAsync(book);

            // Assert
            _mockBookRepository.Verify(repo => repo.UpdateBookAsync(book), Times.Once);
        }

        [Fact]
        public async Task UpdateBookAsync_BookDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Updated Book" };

            _mockBookRepository.Setup(repo => repo.BookExistsAsync(1))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookService.UpdateBookAsync(book));
        }

        [Fact]
        public async Task DeleteBookAsync_BookExists_DeletesBook()
        {
            // Arrange
            _mockBookRepository.Setup(repo => repo.BookExistsAsync(1))
                .ReturnsAsync(true);

            _mockBookRepository.Setup(repo => repo.DeleteBookAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            await _bookService.DeleteBookAsync(1);

            // Assert
            _mockBookRepository.Verify(repo => repo.DeleteBookAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteBookAsync_BookDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            _mockBookRepository.Setup(repo => repo.BookExistsAsync(1))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookService.DeleteBookAsync(1));
        }

        [Fact]
        public async Task BookExistsAsync_ReturnsTrue()
        {
            // Arrange
            _mockBookRepository.Setup(repo => repo.BookExistsAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _bookService.BookExistsAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task BookExistsAsync_ReturnsFalse()
        {
            // Arrange
            _mockBookRepository.Setup(repo => repo.BookExistsAsync(1))
                .ReturnsAsync(false);

            // Act
            var result = await _bookService.BookExistsAsync(1);

            // Assert
            Assert.False(result);
        }
    }
}

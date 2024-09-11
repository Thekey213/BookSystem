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
    public class AuthorServiceTests
    {
        private readonly Mock<IAuthorRepository> _mockAuthorRepository;
        private readonly IAuthorService _authorService;

        public AuthorServiceTests()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _authorService = new AuthorService(_mockAuthorRepository.Object);
        }

        [Fact]
        public async Task GetAllAuthorsAsync_ReturnsListOfAuthors()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { Id = 1, Name = "Author 1" },
                new Author { Id = 2, Name = "Author 2" }
            };

            _mockAuthorRepository.Setup(repo => repo.GetAllAuthorsAsync())
                .ReturnsAsync(authors);

            // Act
            var result = await _authorService.GetAllAuthorsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, a => a.Name == "Author 1");
            Assert.Contains(result, a => a.Name == "Author 2");
        }

        [Fact]
        public async Task GetAuthorByIdAsync_ReturnsAuthor()
        {
            // Arrange
            var author = new Author { Id = 1, Name = "Author 1" };

            _mockAuthorRepository.Setup(repo => repo.GetAuthorByIdAsync(1))
                .ReturnsAsync(author);

            // Act
            var result = await _authorService.GetAuthorByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Author 1", result.Name);
        }

        [Fact]
        public async Task AddAuthorAsync_CallsRepositoryAddAuthorAsync()
        {
            // Arrange
            var author = new Author { Id = 1, Name = "New Author" };

            _mockAuthorRepository.Setup(repo => repo.AddAuthorAsync(author))
                .Returns(Task.CompletedTask);

            // Act
            await _authorService.AddAuthorAsync(author);

            // Assert
            _mockAuthorRepository.Verify(repo => repo.AddAuthorAsync(author), Times.Once);
        }

        [Fact]
        public async Task UpdateAuthorAsync_AuthorExists_UpdatesAuthor()
        {
            // Arrange
            var author = new Author { Id = 1, Name = "Updated Author" };

            _mockAuthorRepository.Setup(repo => repo.AuthorExistsAsync(1))
                .ReturnsAsync(true);

            _mockAuthorRepository.Setup(repo => repo.UpdateAuthorAsync(author))
                .Returns(Task.CompletedTask);

            // Act
            await _authorService.UpdateAuthorAsync(author);

            // Assert
            _mockAuthorRepository.Verify(repo => repo.UpdateAuthorAsync(author), Times.Once);
        }

        [Fact]
        public async Task UpdateAuthorAsync_AuthorDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var author = new Author { Id = 1, Name = "Updated Author" };

            _mockAuthorRepository.Setup(repo => repo.AuthorExistsAsync(1))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _authorService.UpdateAuthorAsync(author));
        }

        [Fact]
        public async Task DeleteAuthorAsync_AuthorExists_DeletesAuthor()
        {
            // Arrange
            _mockAuthorRepository.Setup(repo => repo.AuthorExistsAsync(1))
                .ReturnsAsync(true);

            _mockAuthorRepository.Setup(repo => repo.DeleteAuthorAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            await _authorService.DeleteAuthorAsync(1);

            // Assert
            _mockAuthorRepository.Verify(repo => repo.DeleteAuthorAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteAuthorAsync_AuthorDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            _mockAuthorRepository.Setup(repo => repo.AuthorExistsAsync(1))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _authorService.DeleteAuthorAsync(1));
        }

        [Fact]
        public async Task GetAuthorsWithBooksAsync_ReturnsListOfAuthorsWithBooks()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { Id = 1, Name = "Author 1" },
                new Author { Id = 2, Name = "Author 2" }
            };

            _mockAuthorRepository.Setup(repo => repo.GetAuthorsWithBooksAsync())
                .ReturnsAsync(authors);

            // Act
            var result = await _authorService.GetAuthorsWithBooksAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, a => a.Name == "Author 1");
            Assert.Contains(result, a => a.Name == "Author 2");
        }


    }
}

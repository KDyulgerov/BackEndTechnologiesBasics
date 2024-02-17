using LibroConsoleAPI.Business;
using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Data.Models;
using LibroConsoleAPI.DataAccess;
using LibroConsoleAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;
using System.Diagnostics;

namespace LibroConsoleAPI.IntegrationTests.NUnit
{
    public  class IntegrationTests
    {
        private TestLibroDbContext dbContext;
        private BookManager bookManager;

        [SetUp]
        public void SetUp()
        {
            string dbName = $"TestDb_{Guid.NewGuid()}";
            this.dbContext = new TestLibroDbContext(dbName);
            this.bookManager = new BookManager(new BookRepository(this.dbContext));
        }

        [TearDown]
        public void TearDown()
        {
            this.dbContext.Dispose();
        }

        [Test]
        public async Task AddBookAsync_ShouldAddBook()
        {
            var newBook = new Book
            {
                Title = "Test Book Title",
                Author = "Test Author Name",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction",
                Pages = 300,
                Price = 19.99
            };

            await bookManager.AddAsync(newBook);

            var bookInDb = await dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == newBook.ISBN);
            Assert.That(bookInDb, Is.Not.Null);
            Assert.That(bookInDb.Title, Is.EqualTo("Test Book Title"));
            Assert.That(bookInDb.Author, Is.EqualTo("Test Author Name"));
            Assert.That(bookInDb.ISBN, Is.EqualTo("1234567890123"));
            Assert.That(bookInDb.YearPublished, Is.EqualTo(1994));
            Assert.That(bookInDb.Genre, Is.EqualTo("Fiction"));
            Assert.That(bookInDb.Pages, Is.EqualTo(300));
            Assert.That(bookInDb.Price, Is.EqualTo(19.99));
        }

        [Test]
        public async Task AddBookAsync_TryToAddBookWithInvalidTitle_ShouldThrowException()
        {
            var newBook = new Book
            {
                Title = new string ('A', 500),
                Author = "Test Author Name",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction",
                Pages = 300,
                Price = 19.99
            };

            var exception = Assert.ThrowsAsync<ValidationException> (() => bookManager.AddAsync(newBook));

            var bookInDb = await dbContext.Books.FirstOrDefaultAsync();
            Assert.That(bookInDb, Is.Null);
            Assert.That(exception.Message, Is.EqualTo("Book is invalid."));
        }

        [Test]
        public async Task AddBookAsync_TryToAddBookWithInvalidAuthor_ShouldThrowException()
        {
            var newBook = new Book
            {
                Title = "Test Book Title",
                Author = new string ('A', 200),
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction",
                Pages = 300,
                Price = 19.99
            };

            var exception = Assert.ThrowsAsync<ValidationException>(() => bookManager.AddAsync(newBook));

            var bookInDb = await dbContext.Books.FirstOrDefaultAsync();
            Assert.That(bookInDb, Is.Null);
            Assert.That(exception.Message, Is.EqualTo("Book is invalid."));
        }

        [Test]
        public async Task AddBookAsync_TryToAddBookWithInvalidISBN_ShouldThrowException()
        {
            var newBook = new Book
            {
                Title = "Test Book Title",
                Author = "Test Author Name",
                ISBN = "alabala",
                YearPublished = 1994,
                Genre = "Fiction",
                Pages = 300,
                Price = 19.99
            };

            var exception = Assert.ThrowsAsync<ValidationException>(() => bookManager.AddAsync(newBook));

            var bookInDb = await dbContext.Books.FirstOrDefaultAsync();
            Assert.That(bookInDb, Is.Null);
            Assert.That(exception.Message, Is.EqualTo("Book is invalid."));
        }
        [Test]
        public async Task AddBookAsync_TryToAddBookWithInvalidYearPublished_ShouldThrowException()
        {
            var newBook = new Book
            {
                Title = "Test Book Title",
                Author = "Test Author Name",
                ISBN = "1234567890123",
                YearPublished = 19942,
                Genre = "Fiction",
                Pages = 300,
                Price = 19.99
            };

            var exception = Assert.ThrowsAsync<ValidationException>(() => bookManager.AddAsync(newBook));

            var bookInDb = await dbContext.Books.FirstOrDefaultAsync();
            Assert.That(bookInDb, Is.Null);
            Assert.That(exception.Message, Is.EqualTo("Book is invalid."));
        }

        [Test]
        public async Task AddBookAsync_TryToAddBookWithInvalidGenre_ShouldThrowException()
        {
            var newBook = new Book
            {
                Title = "Test Book Title",
                Author = "Test Author Name",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = new string (' ', 51),
                Pages = 300,
                Price = 19.99
            };

            var exception = Assert.ThrowsAsync<ValidationException>(() => bookManager.AddAsync(newBook));

            var bookInDb = await dbContext.Books.FirstOrDefaultAsync();
            Assert.That(bookInDb, Is.Null);
            Assert.That(exception.Message, Is.EqualTo("Book is invalid."));
        }

        [Test]
        public async Task AddBookAsync_TryToAddBookWithInvalidPagesNumber_ShouldThrowException()
        {
            var newBook = new Book
            {
                Title = "Test Book Title",
                Author = "Test Author Name",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction",
                Pages = 0,
                Price = 19.99
            };

            var exception = Assert.ThrowsAsync<ValidationException>(() => bookManager.AddAsync(newBook));

            var bookInDb = await dbContext.Books.FirstOrDefaultAsync();
            Assert.That(bookInDb, Is.Null);
            Assert.That(exception.Message, Is.EqualTo("Book is invalid."));
        }

        [Test]
        public async Task AddBookAsync_TryToAddBookWithInvalidPrice_ShouldThrowException()
        {
            var newBook = new Book
            {
                Title = "Test Book Title",
                Author = "Test Author Name",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction",
                Pages = 300,
                Price = 0.00
            };

            var exception = Assert.ThrowsAsync<ValidationException>(() => bookManager.AddAsync(newBook));

            var bookInDb = await dbContext.Books.FirstOrDefaultAsync();
            Assert.That(bookInDb, Is.Null);
            Assert.That(exception.Message, Is.EqualTo("Book is invalid."));
        }

        [Test]
        public async Task DeleteBookAsync_WithValidISBN_ShouldRemoveBookFromDb()
        {
            var newBook = new Book
            {
                Title = "Test Book Title",
                Author = "Test Author Name",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction",
                Pages = 300,
                Price = 19.99
            };

            await bookManager.AddAsync(newBook);

            await bookManager.DeleteAsync(newBook.ISBN);

            var bookInDb = await dbContext.Books.FirstOrDefaultAsync();

            Assert.That(bookInDb, Is.Null);
        }

        [Test]
        public async Task DeleteBookAsync_TryToDeleteWithWhiteSpaceISBN_ShouldThrowException()
        {
            var newBook = new Book
            {
                Title = "Test Book Title",
                Author = "Test Author Name",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction",
                Pages = 300,
                Price = 19.99
            };

            await bookManager.AddAsync(newBook);
            const string invalidISBN = "   ";

            var exception = Assert.ThrowsAsync<ArgumentException> (() => bookManager.DeleteAsync(invalidISBN));
            Assert.That(exception.Message, Is.EqualTo("ISBN cannot be empty."));
        }

        [Test]
        public async Task DeleteBookAsync_TryToDeleteWithNullISBN_ShouldThrowException()
        {
            var newBook = new Book
            {
                Title = "Test Book Title",
                Author = "Test Author Name",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction",
                Pages = 300,
                Price = 19.99
            };

            await bookManager.AddAsync(newBook);
            const string? invalidISBN = null;

            var exception = Assert.Throws<ArgumentException>(() => bookManager.DeleteAsync(invalidISBN));
            Assert.That(exception.Message, Is.EqualTo("ISBN cannot be empty."));
        }

        [Test]
        public async Task GetAllAsync_WhenBooksExist_ShouldReturnAllBooks()
        {
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);

            var result = await bookManager.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(10));

            var bookInDb = await bookManager.GetSpecificAsync("9780765316323");
            Assert.That(result.Any(b => b.Title == bookInDb.Title), Is.True);
        }

        [Test]
        public async Task GetAllAsync_WhenNoBooksExist_ShouldThrowKeyNotFoundException()
        {
            var exception = Assert.ThrowsAsync<KeyNotFoundException> (() => bookManager.GetAllAsync());

            Assert.That(exception.Message, Is.EqualTo("No books found."));
        }

        [Test]
        public async Task SearchByTitleAsync_WithValidTitleFragment_ShouldReturnMatchingBooks()
        {
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);
            const string titleFragment = "War and Peace";
            var result = await bookManager.SearchByTitleAsync(titleFragment);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));

            var resultBook = result.First();

            Assert.That(resultBook.Title, Is.EqualTo("War and Peace"));
            Assert.That(resultBook.ISBN, Is.EqualTo("9780140449276"));

        }

        [Test]
        public async Task SearchByTitleAsync_WithInvalidTitleFragment_ShouldThrowKeyNotFoundException()
        {
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);
            const string? titleFragment = "invalidParam";

            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => bookManager.SearchByTitleAsync(titleFragment));

            Assert.That(exception.Message, Is.EqualTo("No books found with the given title fragment."));
        }

        [Test]
        public async Task SearchByTitleAsync_WithNullFragment_ShouldArgumentException()
        {
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);
            const string? titleFragment = null;

            var exception = Assert.ThrowsAsync<ArgumentException>(() => bookManager.SearchByTitleAsync(titleFragment));

            Assert.That(exception.Message, Is.EqualTo("Title fragment cannot be empty."));
        }

        [Test]
        public async Task SearchByTitleAsync_WithWhiteSpaceFragment_ShouldArgumentException()
        {
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);
            const string? titleFragment = "     ";

            var exception = Assert.ThrowsAsync<ArgumentException>(() => bookManager.SearchByTitleAsync(titleFragment));

            Assert.That(exception.Message, Is.EqualTo("Title fragment cannot be empty."));
        }

        [Test]
        public async Task GetSpecificAsync_WithValidIsbn_ShouldReturnBook()
        {
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);

            var result = await bookManager.GetSpecificAsync("9780385487256");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("To Kill a Mockingbird"));
            Assert.That(result.Author, Is.EqualTo("Harper Lee"));
            Assert.That(result.YearPublished, Is.EqualTo(1960));
            Assert.That(result.Genre, Is.EqualTo("Novel"));
            Assert.That(result.Pages, Is.EqualTo(336));
            Assert.That(result.Price, Is.EqualTo(10.99));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidIsbn_ShouldThrowKeyNotFoundException()
        {
            await DatabaseSeeder.SeedDatabaseAsync(dbContext, bookManager);

            const string? isbnNotExisting = "1234567890123";

            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => bookManager.GetSpecificAsync(isbnNotExisting));

            Assert.That(exception.Message, Is.EqualTo($"No book found with ISBN: {isbnNotExisting}"));
        }

        [Test]
        public async Task UpdateAsync_WithValidBook_ShouldUpdateBook()
        {
            var bookInDb = new Book
            {
                Title = "Test Book",
                Author = "Test Author Name",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction",
                Pages = 300,
                Price = 19.99
            };

            await bookManager.AddAsync(bookInDb);

            var newParamsBook = new Book
            {
                Title = "Test Book Title UPDATED",
                Author = "Test Author Name UPDATED",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction UPDATED",
                Pages = 400,
                Price = 29.99
            };

            await bookManager.UpdateAsync(newParamsBook);

            var result = await bookManager.SearchByTitleAsync("Test Book Title UPDATED");

            Assert.That(result.FirstOrDefault().Title, Is.EqualTo(newParamsBook.Title));
            Assert.That(result.FirstOrDefault().Author, Is.EqualTo(newParamsBook.Author));
            Assert.That(result.FirstOrDefault().Genre, Is.EqualTo(newParamsBook.Genre));
            Assert.That(result.FirstOrDefault().Pages, Is.EqualTo(newParamsBook.Pages));
            Assert.That(result.FirstOrDefault().Price, Is.EqualTo(newParamsBook.Price));
        }

        [Test]
        public async Task UpdateAsync_WithInvalidBook_ShouldThrowValidationException()
        {
            var bookInDb = new Book
            {
                Title = "Test Book",
                Author = "Test Author Name",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction",
                Pages = 300,
                Price = 19.99
            };

            await bookManager.AddAsync(bookInDb);

            var newParamsBook = new Book
            {
                Title = new string ('A', 500),
                Author = "Test Author Name UPDATED",
                ISBN = "1234567890123",
                YearPublished = 1994,
                Genre = "Fiction UPDATED",
                Pages = 400,
                Price = 29.99
            };

            var result = Assert.ThrowsAsync<ValidationException>(() => bookManager.UpdateAsync(newParamsBook));

            Assert.That(result.Message, Is.EqualTo("Book is invalid."));
        }

    }
}

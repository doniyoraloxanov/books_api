using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer
{
    public class BookDAL
    {
        public async Task<List<string>> GetAllBooks(int pageNumber = 1, int pageSize = 1000)
        {
            var db = new BookDbContext();
            var skipResult = (pageNumber - 1) * pageSize;

            var bookTitles = await db.Books
                .OrderByDescending(b => (b.ViewCount * 0.5) + ((DateTime.Now.Year - b.PublicationYear) * 2))
                .Skip(skipResult)
                .Take(pageSize)
                .Select(b => b.Title) 
                .ToListAsync();

            return bookTitles;
        }


        public async Task<Book?> GetBookById(Guid id)
        {
            var db = new BookDbContext();

            var exsistingBook = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (exsistingBook == null)
            {
                return null;
            }

            exsistingBook.ViewCount++;
            await db.SaveChangesAsync();
            return exsistingBook;
        }


        public async Task<Book?> UpdateBook(Guid id, Book updateBook)
        {
            var db = new BookDbContext();

            var exsistingBook = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (exsistingBook == null)
            {
                return null;
            }

            exsistingBook.Title = updateBook.Title;
            exsistingBook.PublicationYear = updateBook.PublicationYear;
            exsistingBook.AuthorName = updateBook.AuthorName;
            await db.SaveChangesAsync();
            return exsistingBook;
        }


        
        public async Task<Book?> CreateBook(Book book)
        {
            var db = new BookDbContext();

            var books = await db.Books.FirstOrDefaultAsync(b => b.Title == book.Title);
            if (books != null)
            {
                throw new InvalidOperationException("A book with the same title already exists.");
            }

            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                AuthorName = book.AuthorName,
                IsDeleted = false
            };

            await db.AddAsync(newBook);
            return newBook;
        }


        public async Task<List<Book>> CreateBooksBulk(List<Book> books)
        {
            var db = new BookDbContext();

            var bookTitles = books.Select(b => b.Title).ToList();
            var existingBooks = await db.Books.Where(b => bookTitles.Contains(b.Title)).ToListAsync();

            if (existingBooks.Any()) throw new InvalidOperationException("One or more books with the same title already exist.");
            await db.BulkInsertAsync(books);

            return books;
        }



        public async Task DeleteBook(Guid id)
        {
            var db = new BookDbContext();
            var exsistingBook = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (exsistingBook != null)
            {
                exsistingBook.IsDeleted = true;
                await db.SaveChangesAsync();
            }

        }


        public async Task DeleteBooksBulk(List<Guid> ids)
        {
            var db = new BookDbContext();

            await db.Books
           .Where(b => ids.Contains(b.Id))
           .ExecuteUpdateAsync(b => b.SetProperty(b => b.IsDeleted, true));
            await db.SaveChangesAsync();
        }

    }
}

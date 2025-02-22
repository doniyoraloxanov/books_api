using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer
{
    public class BookDAL
    {

        private readonly BookDbContext _dbContext;

        public BookDAL(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<List<Book>> GetAllBooks(int pageNumber = 1, int pageSize = 1000)
        {
            return await _dbContext.Books
                .OrderByDescending(b => (b.ViewCount * 0.5) + ((DateTime.Now.Year - b.PublicationYear) * 2))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Book?> GetBookById(Guid id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null) return null;

            book.ViewCount++;
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> UpdateBook(Guid id, Book updatedBook)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null) return null;

            book.Title = updatedBook.Title;
            book.PublicationYear = updatedBook.PublicationYear;
            book.AuthorName = updatedBook.AuthorName;
            book.ViewCount = updatedBook.ViewCount;

            await _dbContext.SaveChangesAsync();
            return book;
        }


        public async Task CreateBook(Book book)
        {
            if (await _dbContext.Books.AnyAsync(b => b.Title == book.Title))
            {
                throw new InvalidOperationException("A book with the same title already exists.");
            }

            book.Id = Guid.NewGuid();
            book.IsDeleted = false;

            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }


        public async Task CreateBooksBulk(List<Book> books) => await _dbContext.BulkInsertAsync(books);
    
        public async Task DeleteBook(Guid id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book != null)
            {
                book.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
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

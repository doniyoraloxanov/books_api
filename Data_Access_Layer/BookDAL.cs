using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Migrations;
using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer
{
    public class BookDAL
    {
        public async Task<List<Book>> GetAllBooks()
        {
            var db = new BookDbContext();
            var books = await db.Books.ToListAsync();

            return books;
        }


        public async Task<Book> GetBookById(Guid id)
        {
            var db = new BookDbContext();

            var exsistingBook = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (exsistingBook == null)
            {
                return null;
            }

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

            exsistingBook.Title  = updateBook.Title;
            exsistingBook.PublicationYear = updateBook.PublicationYear;
            exsistingBook.AuthorName = updateBook.AuthorName;
            exsistingBook.ViewCount = updateBook.ViewCount;
            await db.SaveChangesAsync();
            return exsistingBook;
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

      
        public async Task SoftDeleteBooksBulk(List<Guid> ids)
        {
            var db = new BookDbContext();

            await db.Books
           .Where(b => ids.Contains(b.Id))
           .ExecuteUpdateAsync(b => b.SetProperty(b => b.IsDeleted, true));
            await db.SaveChangesAsync();
        }




        public async Task CreateBook(Book book)
        {
            var db = new BookDbContext();
            await db.AddAsync(book);
            await db.SaveChangesAsync();
        }

        public async Task CreateBooksBulk(List<Book> books)
        {
            var db = new BookDbContext();
            await db.BulkInsertAsync(books);
        }
    }
}

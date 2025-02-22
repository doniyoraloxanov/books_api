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
        public async Task<List<Book>> GetAllBooks(int pageNumber = 1, int pageSize = 1000)
        {
            var db = new BookDbContext();
            var skipResult = (pageNumber - 1) * pageSize;

            var bookTitles = await db.Books
                .Skip(skipResult)
                .Take(pageSize)
                .OrderByDescending(b => (b.ViewCount * 0.5) + ((DateTime.Now.Year - b.PublicationYear) * 2))
                .ToListAsync();

            return bookTitles;
        }


        public async Task<Book> GetBookById(Guid id)
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

      
        public async Task DeleteBooksBulk(List<Guid> ids)
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

            // if book title exists, throw error
            var existingBook = await db.Books.FirstOrDefaultAsync(b => b.Title == book.Title);
            if (existingBook != null)
            {
                throw new InvalidOperationException("A book with the same title already exists.");
            }

            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                AuthorName = book.AuthorName,
                ViewCount = book.ViewCount,
                IsDeleted = false
            };

            await db.AddAsync(newBook);
            await db.SaveChangesAsync();
        }

        public async Task CreateBooksBulk(List<Book> books)
        {
            var db = new BookDbContext();
            await db.BulkInsertAsync(books);
        }
    }
}

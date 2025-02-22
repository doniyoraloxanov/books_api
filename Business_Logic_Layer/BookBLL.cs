using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Entities;

namespace Business_Logic_Layer
{
    public class BookBLL
    {

        private Data_Access_Layer.BookDAL _DAL;
        private Mapper _BooksMapper;
        private Mapper _AddBooksMapper;
        private Mapper _UpdateBooksMapper;


        public BookBLL()
        {
           _DAL = new Data_Access_Layer.BookDAL();
            var _configBook = new MapperConfiguration(cfg => cfg.CreateMap<Book, BooksModel>().ReverseMap());
            var _configAddBook = new MapperConfiguration(cfg => cfg.CreateMap<Book, AddBooksModel>().ReverseMap());
            var _configUpdateBook = new MapperConfiguration(cfg => cfg.CreateMap<Book, UpdateBooksModel>().ReverseMap());



            _BooksMapper = new Mapper(_configBook);
            _AddBooksMapper = new Mapper(_configAddBook);
            _UpdateBooksMapper = new Mapper(_configUpdateBook);


        }


        public async  Task<List<BooksModel>> GetAllBooks()
        {
            List<Book> booksFromDB =  await _DAL.GetAllBooks();
            List<BooksModel> booksModel = _BooksMapper.Map<List<Book>, List<BooksModel>>(booksFromDB);

            return booksModel;
        }


        public async  Task<BooksModel> GetBookById(Guid id)
        {
            var bookEntity =  await _DAL.GetBookById(id);

            BooksModel bookModel = _BooksMapper.Map<Book, BooksModel>(bookEntity);

       
            return  bookModel;

        }


        public async Task<UpdateBooksModel?> UpdateBook(Guid id, UpdateBooksModel updateBooks)
        {
            var bookModel = _UpdateBooksMapper.Map<UpdateBooksModel, Book>(updateBooks);

            var bookEntity = await _DAL.UpdateBook(id, bookModel);

            var updatedBookModel = _UpdateBooksMapper.Map<Book, UpdateBooksModel>(bookEntity);

            return updatedBookModel;
        }


        public async Task DeleteBook(Guid id)
        {

            await _DAL.DeleteBook(id);

        }



        // Code softDleteBooks bulk 
        public async Task DeleteSoftBook(List<Guid> ids)
        {
            await _DAL.SoftDeleteBooksBulk(ids);
        }


        public async Task CreateBook(AddBooksModel booksModel)
        {
           var bookEntity = _AddBooksMapper.Map<AddBooksModel, Book>(booksModel);
           await _DAL.CreateBook(bookEntity);
        }


        public async Task CreateBooksBulk(List<AddBooksModel> addBooksModels)
        {
            var bookEntities = _AddBooksMapper.Map<List<AddBooksModel>, List<Book>>(addBooksModels);
            await _DAL.CreateBooksBulk(bookEntities);
        }

    }
}

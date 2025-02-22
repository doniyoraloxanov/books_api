﻿using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Entities;


namespace Business_Logic_Layer
{
    public class BookBLL
    {
        private readonly Data_Access_Layer.BookDAL _DAL;
        private readonly IMapper _mapper;

        public BookBLL()
        {
            _DAL = new Data_Access_Layer.BookDAL();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BooksModel>().ReverseMap();
                cfg.CreateMap<Book, AddBooksModel>().ReverseMap();
                cfg.CreateMap<Book, UpdateBooksModel>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }



        public async Task<List<BooksModel>> GetAllBooks(int pageNumber = 1, int pageSize = 1000)
        {
            var booksFromDB = await _DAL.GetAllBooks(pageNumber, pageSize);
            return _mapper.Map<List<BooksModel>>(booksFromDB);
        }



        public async Task<BooksModel> GetBookById(Guid id)
        {
            var bookEntity = await _DAL.GetBookById(id);
            return _mapper.Map<BooksModel>(bookEntity);
        }


        public async Task CreateBook(AddBooksModel booksModel)
        {
            var bookEntity = _mapper.Map<Book>(booksModel);
            await _DAL.CreateBook(bookEntity);
        }


        public async Task CreateBooksBulk(List<AddBooksModel> addBooksModels)
        {
            var bookEntities = _mapper.Map<List<Book>>(addBooksModels);
            await _DAL.CreateBooksBulk(bookEntities);
        }



        public async Task<UpdateBooksModel?> UpdateBook(Guid id, UpdateBooksModel updateBooks)
        {
            var bookEntity = _mapper.Map<Book>(updateBooks);
            var updatedEntity = await _DAL.UpdateBook(id, bookEntity);
            return _mapper.Map<UpdateBooksModel>(updatedEntity);
        }



        public async Task DeleteBook(Guid id) => await _DAL.DeleteBook(id);

        public async Task DeleteSoftBook(List<Guid> ids) => await _DAL.DeleteBooksBulk(ids);

    }
}

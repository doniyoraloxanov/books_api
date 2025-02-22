using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

  
        private Business_Logic_Layer.BookBLL _BLL;
        public BooksController()
        {
            _BLL = new Business_Logic_Layer.BookBLL();
        }


        // GET | All books

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _BLL.GetAllBooks();
            return Ok(books);
        }




        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid id)
        {
            var exsistingBook = await _BLL.GetBookById(id);

            if (exsistingBook == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(exsistingBook);
        }




        // POST | Create book
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddBooksModel addBooksModel)
        {
            
            if(addBooksModel == null)
            {
                return NotFound();
            }

            await _BLL.CreateBook(addBooksModel);
            return Ok("A book inserted successfully.");
        }


        [HttpPost("bulk-create")]
        public async Task<IActionResult> CreateBooksBulk([FromBody] List<AddBooksModel> addBooksModels)
        {
            if (addBooksModels == null)
            {
              return NotFound();
            }

            await _BLL.CreateBooksBulk(addBooksModels);
            return Ok("Books inserted successfully.");
        }


        // UPDATE  | Update single book
        // PUT 

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, [FromBody] UpdateBooksModel updateBooksModel)
        {
            var updatedBook = await _BLL.UpdateBook(id, updateBooksModel);

            if (updatedBook == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(updatedBook);
        }

        //#############################################


        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> SoftDeleteBook([FromRoute] Guid id)
        {
            var deletedBook = _BLL.DeleteBook(id);
            if (deletedBook == null)
            {
                return NotFound("Invalid ID");
            }
            return Ok("Book deleted successfully.");
        }


        //#############################################
        [HttpDelete]
        [Route("bulk-delete")]
        public async Task<IActionResult> SoftDeleteBooksBulk([FromBody] List<Guid> ids)
        {
            if (ids == null)
            {
                return NotFound();
            }

            await _BLL.DeleteSoftBook(ids);

            return Ok("Books deleted successfully.");
        }

    }
}

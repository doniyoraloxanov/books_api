using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;


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



        /// <summary>
        /// Retrieves paginated book titles sorted by popularity.
        /// </summary>
        /// <param name="pageNumber">Page number (default: 1).</param>
        /// <param name="pageSize">Items per page (default: 1000).</param>
        /// <returns>List of book titles.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000)
        {
            var bookTitles = await _BLL.GetAllBooks(pageNumber, pageSize);
            return Ok(bookTitles);
        }

        /// <summary>
        /// Retrieves book details by ID.
        /// </summary>
        /// <param name="id">Unique identifier of the book.</param>
        /// <returns>Book details if found.</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid id)
        {
            var existingBook = await _BLL.GetBookById(id);
            if (existingBook == null)
                return NotFound("Invalid ID");

            return Ok(existingBook);
        }


        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="addBooksModel">Book details.</param>
        /// <returns>The created book.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] AddBooksModel addBooksModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _BLL.CreateBook(addBooksModel);
            return Ok(book);
        }

        /// <summary>
        /// Creates multiple books in bulk.
        /// </summary>
        /// <param name="addBooksModels">List of books to add.</param>
        /// <returns>List of created books.</returns>
        [HttpPost("bulk-create")]
        public async Task<IActionResult> CreateBooksBulk([FromBody] List<AddBooksModel> addBooksModels)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var books = await _BLL.CreateBooksBulk(addBooksModels);
            return Ok(books);
        }



        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <param name="updateBooksModel">Updated book details.</param>
        /// <returns>The updated book.</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, [FromBody] UpdateBooksModel updateBooksModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedBook = await _BLL.UpdateBook(id, updateBooksModel);
            return Ok(updatedBook);
        }

        /// <summary>
        /// Soft deletes a book by ID by marking it as deleted instead of removing it from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>
        /// Returns a success message if the book is successfully soft deleted.
        /// Returns 404 if the book does not exist or is already deleted.
        /// </returns>
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
        {
            var deletedBook = _BLL.DeleteBook(id);
            if (deletedBook == null)
            {
                return NotFound("Invalid ID");
            }
            return Ok("A Book deleted successfully!");
        }




        /// <summary>
        /// Soft deletes multiple books by marking them as deleted.
        /// </summary>
        /// <param name="ids">List of book IDs.</param>
        /// Returns a success message if the boooks are successfully soft deleted.
        [HttpDelete]
        [Route("bulk-delete")]
        public async Task<IActionResult> DeleteBooksBulk([FromBody] List<Guid> ids)
        {
            await _BLL.DeleteBooksBulk(ids);

            return Ok("Books deleted successfully.");
        }

    }
}

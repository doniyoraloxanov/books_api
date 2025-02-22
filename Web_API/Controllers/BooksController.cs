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



        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000)
        {
            var books = await _BLL.GetAllBooks(pageNumber, pageSize);
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



        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] AddBooksModel addBooksModel)
        {

            if (ModelState.IsValid)
            {

                await _BLL.CreateBook(addBooksModel);
                return Ok("A book inserted successfully.");

            }
            else
            {
                 return BadRequest(ModelState);
            }



        }


        [HttpPost("bulk-create")]
        public async Task<IActionResult> CreateBooksBulk([FromBody] List<AddBooksModel> addBooksModels)
        {
            if (ModelState.IsValid)
            {
                await _BLL.CreateBooksBulk(addBooksModels);
                return Ok("Books inserted successfully.");

            } else
            {
                return BadRequest(ModelState);
            }

          
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, [FromBody] UpdateBooksModel updateBooksModel)
        {

            if (ModelState.IsValid)
            {
                var updatedBook = await _BLL.UpdateBook(id, updateBooksModel);

                return Ok(updatedBook);
            }
            else
            {
                return BadRequest(ModelState);
            }
           
        }



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
         

        [HttpDelete]
        [Route("bulk-delete")]
        public async Task<IActionResult> DeleteBooksBulk([FromBody] List<Guid> ids)
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

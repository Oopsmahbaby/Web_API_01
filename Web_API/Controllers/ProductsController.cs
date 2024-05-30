using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API.Helpers;
using Web_API.Models;
using Web_API.Repositories;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;

        public ProductsController(IBookRepository repo)
        {
            _bookRepo = repo;
        }

        [HttpGet]
        //[Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> getAllBooks()
        {
            try
            {
                return Ok(await _bookRepo.getAllBooksAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getBookByID(int id)
        {
            var book = await _bookRepo.getBookAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> addNewBook(BookModel model)
        {
            try
            {
                var newBookID = await _bookRepo.addBookAsync(model);
                var book = await _bookRepo.getBookAsync(newBookID);

                return book == null ? NotFound() : Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> updateBook(int id, BookModel model)
        {
            try
            {
                //if (id != model.Id) return NotFound();
                //var updateResult = await _bookRepo.updateBookAsync(id, model);
                //var result = await _bookRepo.getBookAsync(updateResult);

                //return result == null ? NotFound() : Ok();
                if (id != model.Id)
                {
                    return NotFound();
                }

                try
                {
                    // Attempt to update the book
                    await _bookRepo.updateBookAsync(id, model);
                    // If the update succeeds, return Ok
                    return Ok();
                }
                catch
                {
                    // If an exception occurs during the update, return NotFound
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> deleteBook(int id)
        {
            try
            {
                try
                {
                    await _bookRepo.deleteBookAsync(id);
                    return Ok();
                }
                catch
                {
                    return NotFound();
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}

using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Serdiuk.API.Controllers.Base;
using Serdiuk.BookShop.Domain.Models.Requests.Books;
using Serdiuk.Services.Interfaces;

namespace Serdiuk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : BaseApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("by-filter")]
        public async Task<IActionResult> GetBooksByFilterAsync([FromQuery]GetBooksByFilterRequest request)
        {
            var books = await _bookService.GetBooksByFilterAsync(request);
            
            HandleResult(books);
            return Ok(books.Value);
        }
        [HttpPost("upload-cover")]
        public async Task<IActionResult> UploadBookCoverAsync(IFormFile file, Guid id)
        {
            var result = await _bookService.UploadBookCoverAsync(file, id);
            HandleResult(result);

            return Ok();
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateBookAsync([FromForm] CreateBookRequest request)
        {
            var result = await _bookService.CreateBookAsync(request);
            HandleResult(result);
            return Ok();
        }
        [HttpGet("by-page")]
        public async Task<IActionResult> GetBookByPageAsync([FromQuery]int page)
        {
            var result = await _bookService.GetBooksByPageAsync(page);
            HandleResult(result);

            return Ok(result.Value);
        }
       
    }
}

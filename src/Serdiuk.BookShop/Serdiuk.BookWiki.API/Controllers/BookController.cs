using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serdiuk.API.Controllers.Base;
using Serdiuk.BookShop.Domain.IdentityModels;
using Serdiuk.BookShop.Domain.Models.Requests.Books;
using Serdiuk.Services.Interfaces;

namespace Serdiuk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookController : BaseApiController
    {
        private readonly IBookService _bookService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookController(IBookService bookService, UserManager<ApplicationUser> userManager)
        {
            _bookService = bookService;
            _userManager = userManager;
        }
        [HttpGet("by-filter")]
        [AllowAnonymous]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetBooksByFilterAsync([FromQuery]GetBooksByFilterRequest request)
        {
            var books = await _bookService.GetBooksByFilterAsync(request, UserId);
            
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
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadBookImageAsync([FromForm] UploadImageRequest request)
        {
            var result = await _bookService.AddPhotoToBookAsync(request.File, request.Id);
            HandleResult(result);

            return Ok();
        }
        [HttpPut("like")]
        public async Task<IActionResult> LikeBookAsync(Guid bookId)
        {
            var user = await _userManager.Users.Include(x=>x.LikedBooks).FirstOrDefaultAsync(x=>x.Id==UserId);
            if (user == null) return Unauthorized();
            var result = await _bookService.ChangeRatingBook(user, bookId);

            HandleResult(result);
            return Ok(result.Value);

        }
        [HttpDelete("delete-image")]
        public async Task<IActionResult> DeleteBookImageAsync(DeleteImageRequest request)
        {
            var result = await _bookService.RemovePhotoToBookAsync(request.ImageId, request.BookId);
            HandleResult(result);

            return Ok();
        }
        [HttpGet("get-user-like")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetUserLikeBookAsync()
        {
            var user = await _userManager.Users.Include(x => x.LikedBooks).ThenInclude(x=>x.Cover).FirstOrDefaultAsync(x=>x.Id == UserId);

            if(user == null) return NotFound();

            var result = await _bookService.GetUserLikeBookAsync(user);
            HandleResult(result);

            return Ok(result.Value);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateBookAsync([FromForm] CreateBookRequest request)
        {
            var result = await _bookService.CreateBookAsync(request);
            HandleResult(result);
            return Ok();
        }
        [HttpGet("get-by-id/{id}")]
        [AllowAnonymous]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetBookByIdAsync(Guid id)
        {
            var result = await _bookService.GetBookByIdAsync(id);
            HandleResult(result);

            return Ok(result.Value);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serdiuk.API.Controllers.Base;
using Serdiuk.BookShop.Domain.Models.Requests.Authors;
using Serdiuk.Services.Interfaces;

namespace Serdiuk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthorController : BaseApiController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [Authorize(Policy = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAuthorAsync(CreateAuthorRequest request)
        {
            var result = await _authorService.CreateAuthorAsync(request);
            HandleResult(result);

            return Ok();
        }
        [HttpGet("get-all")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetAllAuthorsAsync()
        {
            var result = await _authorService.GetAllAuthorAsync();
            HandleResult(result);

            return Ok(result.Value);
        }
        [HttpGet("get-by-id")]
        //[ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetAuthorByIdAsync(Guid authorId)
        {
            var result = await _authorService.GetAuthorByIdAsync(authorId);
            HandleResult(result);

            return Ok(result.Value);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serdiuk.API.Controllers.Base;
using Serdiuk.BookShop.Domain.IdentityModels;
using Serdiuk.BookShop.Domain.Models.Requests.Comment;
using Serdiuk.Services.Interfaces;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Serdiuk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseApiController
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpPut("like")]
        public async Task<IActionResult> LikeCommentAsync(LikeCommentRequest request)
        {
            var user = await _userManager.Users.Include(x => x.LikedComments).FirstOrDefaultAsync(x=>x.Id==UserId);

            if (user == null) return Unauthorized();

            var result = await _commentService.LikeCommentAsync(request, user);
            HandleResult(result);
            return Ok(result.Value);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CraeteCommentAsync(CreateCommentRequest request)
        {
            var user = await _userManager.Users.Include(x => x.LikedComments).FirstOrDefaultAsync(x => x.Id == UserId);

            if (user == null) return Unauthorized();

            var result = await _commentService.CreateCommentAsync(request, user);
            HandleResult(result);

            return Ok();
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetBookCommentsByIdAsync([FromQuery]GetBookCommentByIdRequest request)
        {
            var result = await _commentService.GetBookCommentsByIdAsync(request);
            HandleResult(result);
            
            return Ok(result.Value);
        }


    }
}

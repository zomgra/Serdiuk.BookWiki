using FluentResults;
using Serdiuk.BookShop.Domain.IdentityModels;
using Serdiuk.BookShop.Domain.Models.Requests.Comment;
using Serdiuk.BookShop.Domain.ViewModels;

namespace Serdiuk.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Result> CreateCommentAsync(CreateCommentRequest request, ApplicationUser user);
        Task<Result<int>> LikeCommentAsync(LikeCommentRequest request, ApplicationUser user);
        Task<Result<List<CommentViewModel>>> GetBookCommentsByIdAsync(GetBookCommentByIdRequest request);
    }
}

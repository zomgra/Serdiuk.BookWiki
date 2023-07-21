using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Serdiuk.BookShop.Domain.IdentityModels;
using Serdiuk.BookShop.Domain.Models;
using Serdiuk.BookShop.Domain.Models.Requests.Comment;
using Serdiuk.BookShop.Domain.ViewModels;
using Serdiuk.Persistance;
using Serdiuk.Services.Interfaces;

namespace Serdiuk.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public CommentService(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> CreateCommentAsync(CreateCommentRequest request, ApplicationUser user)
        {
            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == request.BookId);
                var comment = new Comment
                {
                    Content = request.Content,
                    Likes = 0,
                    WriterUsername = user.UserName,
                };
                book.Comments.Add(comment);

                _context.Users.Append(user);
                user.Comments.Add(comment);
                await _context.SaveChangesAsync(CancellationToken.None);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail("Error with creating comment");
            }

        }

        public async Task<Result<List<CommentViewModel>>> GetBookCommentsByIdAsync(GetBookCommentByIdRequest request)
        {
            try
            {
                var entity = await _context.Books.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == request.BookId);
                if (entity == null) return Result.Fail("Invalid credentials");

                return Result.Ok(_mapper.Map<List<CommentViewModel>>(entity.Comments));
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> LikeCommentAsync(LikeCommentRequest request, ApplicationUser user)
        {
            try
            {
                var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == request.CommentId);
                if (comment == null) return Result.Fail("Invalid credentials");

                if (user.LikedComments.Contains(comment))
                    return Result.Fail("You already like comment");

                comment.Likes++;
                user.LikedComments.Add(comment);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}

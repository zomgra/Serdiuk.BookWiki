using FluentResults;
using Microsoft.EntityFrameworkCore;
using Serdiuk.BookShop.Domain.Models;
using Serdiuk.BookShop.Domain.Models.Requests.Authors;
using Serdiuk.Persistance;
using Serdiuk.Services.Interfaces;

namespace Serdiuk.Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAppDbContext _context;

        public AuthorService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Author> GetAuthorByIdAsync(Guid id)
        {
            var entity = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) throw new Exception("Bad id, author not found"); // TODO: Change to self exception
            return entity;
        }

        public async Task<Result> CreateAuthorAsync(CreateAuthorRequest request)
        {
            try
            {
                var author = new Author
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };
                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync(CancellationToken.None);
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }

        }

        public async Task<Result<List<Author>>> GetAllAuthorAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            return Result.Ok(authors);
        }

        public async Task<Result> UpdateAuthorAsync(UpdateAuthorRequest request)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (author == null) return Result.Fail("Author not found");
            author.FirstName = request.FirstName;
            author.LastName = request.LastName;
            await _context.SaveChangesAsync(CancellationToken.None);
            return Result.Ok();
        }
    }
}

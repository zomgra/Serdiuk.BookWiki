using FluentResults;
using Serdiuk.BookShop.Domain.Models;
using Serdiuk.BookShop.Domain.Models.Requests.Authors;

namespace Serdiuk.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<Result> CreateAuthorAsync(CreateAuthorRequest request);
        Task<Result> UpdateAuthorAsync(UpdateAuthorRequest request);
        Task<Result<List<Author>>> GetAllAuthorAsync();
        Task<Author> GetAuthorByIdAsync(Guid id);
    }
}

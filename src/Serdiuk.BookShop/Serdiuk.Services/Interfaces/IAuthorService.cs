using FluentResults;
using Serdiuk.BookShop.Domain.Models;
using Serdiuk.BookShop.Domain.Models.Requests.Authors;
using Serdiuk.BookShop.Domain.ViewModels;

namespace Serdiuk.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<Result> CreateAuthorAsync(CreateAuthorRequest request);
        Task<Result> UpdateAuthorAsync(UpdateAuthorRequest request);
        Task<Result<List<Author>>> GetAllAuthorAsync();
        Task<Result<AuthorViewModel>> GetAuthorByIdAsync(Guid id,string userId);
    }
}

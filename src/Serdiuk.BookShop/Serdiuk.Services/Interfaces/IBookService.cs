using FluentResults;
using Microsoft.AspNetCore.Http;
using Serdiuk.BookShop.Domain.Models.Requests.Books;
using Serdiuk.BookShop.Domain.ViewModels;

namespace Serdiuk.Services.Interfaces
{
    public interface IBookService
    {
        Task<Result<BookViewModel>> GetBookByIdAsync(Guid id);
        Task<Result<List<BookViewModel>>> GetBooksByFilterAsync(GetBooksByFilterRequest request);
        Task<Result<List<BookViewModel>>> GetBooksByPageAsync(int page);
        Task<Result> UploadBookCoverAsync(IFormFile photo, Guid id);
        Task<Result> AddPhotoToBookAsync(IFormFile photo, Guid id);
        Task<Result> CreateBookAsync(CreateBookRequest request);
        Task<Result> UpdateBookAsync(UpdateBookRequest request);
    }
}

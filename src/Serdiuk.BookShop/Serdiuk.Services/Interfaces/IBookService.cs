﻿using FluentResults;
using Microsoft.AspNetCore.Http;
using Serdiuk.BookShop.Domain.IdentityModels;
using Serdiuk.BookShop.Domain.Models.Requests.Books;
using Serdiuk.BookShop.Domain.ViewModels;

namespace Serdiuk.Services.Interfaces
{
    public interface IBookService
    {
        Task<Result<BookViewModel>> GetBookByIdAsync(Guid id);
        Task<Result<List<BookInfoViewModel>>> GetBooksByFilterAsync(GetBooksByFilterRequest request);
        Task<Result<List<BookInfoViewModel>>> GetBooksByPageAsync(int page);
        Task<Result> UploadBookCoverAsync(IFormFile photo, Guid id);
        Task<Result> AddPhotoToBookAsync(IFormFile photo, Guid id);
        Task<Result> CreateBookAsync(CreateBookRequest request);
        Task<Result> UpdateBookAsync(UpdateBookRequest request);
        Task<Result<int>> ChangeRatingBook(ApplicationUser user, Guid bookId);
    }
}

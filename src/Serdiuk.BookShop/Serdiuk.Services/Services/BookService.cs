using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serdiuk.BookShop.Domain.Models;
using Serdiuk.BookShop.Domain.Models.Requests.Books;
using Serdiuk.BookShop.Domain.ViewModels;
using Serdiuk.Persistance;
using Serdiuk.Services.Interfaces;

namespace Serdiuk.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;

        public BookService(IAppDbContext context, IMapper mapper, IAuthorService service)
        {
            _context = context;
            _mapper = mapper;
            _authorService = service;
        }

        public async Task<Result> AddPhotoToBookAsync(IFormFile photo, Guid id)
        {
            string contentType = photo.ContentType.ToLower();

            if (!contentType.StartsWith("image/"))
            {
                return Result.Fail("Its not a photo");
            }
            var entity = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return Result.Fail("Book not found");
            try
            {
                using (var ms = new MemoryStream())
                {
                    await photo.CopyToAsync(ms);
                    var newPhoto = new Image
                    {
                        Data = ms.ToArray(),
                    };
                    entity.Images.Add(newPhoto);
                    await _context.SaveChangesAsync(CancellationToken.None);
                    return Result.Ok();
                }
            }
            catch
            {
                return Result.Fail("Saving Failure");
            }
        }

        public async Task<Result> CreateBookAsync(CreateBookRequest request)
        {
            var authors = new List<Author>();

            authors = _context.Authors.Where(x => request.Authors.Contains(x.Id)).ToList();

            var book = new Book
            {
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
                Id = Guid.NewGuid(),
                Cover = new(),
            };
            book.Authors = new List<Author>();
            foreach (var item in authors)
            {
                book.Authors.Add(item);
            }
            try
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync(CancellationToken.None);


                var resultUploadFile = await UploadBookCoverAsync(request.File, book.Id);
                if (resultUploadFile.IsSuccess)
                {
                    return Result.Ok();
                }
                else
                {
                    return Result.Fail("Failed with upload cover");
                }
            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException;
                string errorMessage = innerException?.Message;
                return Result.Fail(ex.Message);
            }
        }

        public Task<Result<BookViewModel>> GetBookByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<BookViewModel>>> GetBooksByFilterAsync(GetBooksByFilterRequest request)
        {
            try
            {
                var books = await _context.Books.Where(x=>x.Status == request.Status)
                    .Skip(request.Page * 10)
                    .Take(10).ToListAsync();

                return Result.Ok(_mapper.Map<List<BookViewModel>>(books));
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public async Task<Result<List<BookViewModel>>> GetBooksByPageAsync(int page)
        {
            try
            {
                var books = await _context.Books.Skip(page * 10).Take(10)
                    .Include(x=>x.Authors)
                    .Include(x=>x.Cover)
                    .Include(x=>x.Images)
                    .Include(x=>x.Comments)
                    .ToListAsync();

                return Result.Ok(_mapper.Map<List<BookViewModel>>(books));
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Task<Result> UpdateBookAsync(UpdateBookRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> UploadBookCoverAsync(IFormFile photo, Guid id)
        {
            string contentType = photo.ContentType.ToLower();

            if (!contentType.StartsWith("image/"))
            {
                return Result.Fail("Its not a photo");
            }
            var entity = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return Result.Fail("Book not found");
            try
            {
                using (var ms = new MemoryStream())
                {
                    await photo.CopyToAsync(ms);
                    var newPhoto = new Image
                    {
                        Id = entity.Id,
                        Data = ms.ToArray(),
                    };
                    entity.Cover = newPhoto;
                    await _context.Images.AddAsync(newPhoto);
                    await _context.SaveChangesAsync(CancellationToken.None);
                    return Result.Ok();
                }
            }
            catch
            {
                return Result.Fail("Saving Failure");
            }
        }
    }
}

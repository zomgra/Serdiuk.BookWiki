using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serdiuk.BookShop.Domain.IdentityModels;
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

        public BookService(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<Result<int?>> ChangeRatingBook(ApplicationUser user, Guid bookId)
        {
            var likedBook = user.LikedBooks.FirstOrDefault(x => x.Id == bookId);
            try
            {
                if (likedBook == null)
                {
                    var entity = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);
                    if (entity == null) return Result.Fail("Invalid book id");
                    _context.Users.Attach(user);
                    user.LikedBooks.Add(entity);
                    var count = await _context.SaveChangesAsync(CancellationToken.None);
                    return Result.Ok(entity.Rating);
                }
                else
                {
                    _context.Users.Attach(user);
                    user.LikedBooks.Remove(likedBook);
                    var count = await _context.SaveChangesAsync(CancellationToken.None);
                    return Result.Ok(likedBook.Rating);
                }
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
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
                LikedUsers = new List<ApplicationUser>()
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

        public async Task<Result<BookViewModel>> GetBookByIdAsync(Guid id)
        {
            var entity = await _context.Books.Include(x => x.Authors).Include(x => x.Comments).Include(x => x.Images).Include(x => x.Cover).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return Result.Fail("Book not found");
            return Result.Ok(_mapper.Map<BookViewModel>(entity));
        }

        public async Task<Result<List<BookInfoViewModel>>> GetBooksByFilterAsync(GetBooksByFilterRequest request, string userId)
        {
            try
            {
                var query = _context.Books
                .OrderBy(x => x.Status == request.Status)
                .Where(x => x.Status == request.Status)
                .Skip(request.Page * 10)
                .Take(10);

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
                }

                var books = await query
                    .Include(x => x.Cover)
                    .Include(x => x.Authors)
                    .Include(x => x.LikedUsers)
                    .ToListAsync();

                var mappedBooks = new List<BookInfoViewModel>();

                foreach (var book in books)
                {
                    var userLikeBook = book.LikedUsers.Select(x => x.Id).Contains(userId);
                    var mapBook = _mapper.Map<BookInfoViewModel>(book, opts =>
                    {
                        opts.AfterMap((src, dest) =>
                        {
                            dest.YouLikeIt = userLikeBook;
                        });
                    });
                    mappedBooks.Add(mapBook);
                }

                return Result.Ok(mappedBooks);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public async Task<Result<List<BookInfoViewModel>>> GetUserLikeBookAsync(ApplicationUser user)
        {
            try
            {
                var books = user.LikedBooks;
                var mappedBook = _mapper.Map<List<BookInfoViewModel>>(books);
                return await Task.FromResult(Result.Ok(mappedBook));
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }

        }

        public async Task<Result> RemovePhotoToBookAsync(Guid photoId, Guid id)
        {
            var book = await _context.Books.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);
            if (book == null) return Result.Fail("Invalid book id");
            var image = book.Images.FirstOrDefault(x => x.Id == photoId);
            if (image == null) return Result.Fail("Invalid image id");
            try
            {
                book.Images.Remove(image);
                _context.Images.Remove(image);
                await _context.SaveChangesAsync(CancellationToken.None);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
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

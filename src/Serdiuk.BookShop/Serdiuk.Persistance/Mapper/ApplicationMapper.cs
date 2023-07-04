using AutoMapper;
using Serdiuk.BookShop.Domain.Models;
using Serdiuk.BookShop.Domain.ViewModels;

namespace Serdiuk.Persistance.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Book, BookViewModel>();
            CreateMap<Author, AuthorViewModel>();
            CreateMap<Comment, CommentViewModel>();
        }
    }
}

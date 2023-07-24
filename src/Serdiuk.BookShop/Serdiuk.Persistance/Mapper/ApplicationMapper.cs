using AutoMapper;
using Serdiuk.BookShop.Domain.Models;
using Serdiuk.BookShop.Domain.ViewModels;
using System.Linq;

namespace Serdiuk.Persistance.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Book, BookViewModel>()
            .ForMember(x => x.Name, src => src.MapFrom(z => z.Name))
            .ForMember(x => x.Description, src => src.MapFrom(z => z.Description))
            .ForMember(x => x.Status, src => src.MapFrom(z => z.Status))
            .ForMember(x => x.Cover, src => src.MapFrom(z => z.Cover.Data));

            CreateMap<Book, BookInfoViewModel>()
            .ForMember(dest => dest.Cover, opt => opt.MapFrom(src => src.Cover.Data))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.YouLikeIt, opt => opt.MapFrom((src, dest, destMember, context) =>
            {
                var userId = (string)context.Items["UserId"];
                return src.LikedUsers.Select(x=>x.Id).Contains(userId);
            }));

            CreateMap<Author, AuthorViewModel>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var userId = context?.Items["UserId"]?.ToString() ?? "";

                    return src.Books.Select(book => new BookInfoViewModel
                    {

                        Id = book.Id,
                        Name = book.Name,
                        Description = book.Description,
                        Status = book.Status,
                        Authors = new(),
                        Cover = book.Cover.Data,
                        CommentsCount = book.Comments.Count,
                        Rating = (int)book.Rating,
                        YouLikeIt = context?.Items["UserId"] != null ? book.LikedUsers.Any(user => user.Id == userId) : false,
                    }).ToList();
                }))
                    .AfterMap((src, dest, context) =>
                    {
                        var userId = (string)context.Items["UserId"];
                        if (!string.IsNullOrWhiteSpace(userId))
                        {
                            var likedUserIds = src.Books.SelectMany(book => book.LikedUsers.Select(user => user.Id)).ToHashSet();

                            foreach (var mbook in dest.Books)
                            {
                                mbook.YouLikeIt = likedUserIds.Contains(userId);
                            }
                        }
                    });

            CreateMap<Comment, CommentViewModel>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.WriterUsername, opt => opt.MapFrom(src => src.WriterUsername))
            .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes));
        }
    }
}

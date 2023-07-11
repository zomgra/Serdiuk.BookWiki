﻿using AutoMapper;
using Serdiuk.BookShop.Domain.Models;
using Serdiuk.BookShop.Domain.ViewModels;

namespace Serdiuk.Persistance.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Book, BookViewModel>()
            .ForMember(dest => dest.Cover, opt => opt.MapFrom(src => src.Cover.Data))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(x => x.Data)))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));

            CreateMap<Book, BookInfoViewModel>()
            .ForMember(dest => dest.Cover, opt => opt.MapFrom(src => src.Cover.Data))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count()));

            CreateMap<Author, AuthorViewModel>()
           .ForMember(dest => dest.Books, opt => opt.Ignore());

            CreateMap<Comment, CommentViewModel>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.WriterUsername, opt => opt.MapFrom(src => src.WriterUsername))
            .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes));
        }
    }
}

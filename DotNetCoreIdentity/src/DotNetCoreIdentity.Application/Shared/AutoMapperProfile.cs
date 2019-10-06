using AutoMapper;
using DotNetCoreIdentity.Application.BlogServices.Dtos;
using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using DotNetCoreIdentity.Domain.BlogEntries;
using DotNetCoreIdentity.Domain.PostTypes;
using System;
using System.Text.RegularExpressions;

namespace DotNetCoreIdentity.Application.Shared
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryInput, Category>()
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore());


            // Post Service

            CreateMap<Post, PostDto>()
                .ForMember(x => x.Category, opt => opt.MapFrom<Category>(c => c.Category))
                .ForMember(x => x.PlainContent, opt => opt.MapFrom(s => RemoveHTMLTags(s.Content)));
            CreateMap<PostDto, Post>()
                .ForMember(x => x.Category, opt => opt.MapFrom<CategoryDto>(c => c.Category));

            CreateMap<CreatePostInput, Post>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            CreateMap<UpdatePostInput, Post>()
                .ForMember(x => x.Id, opt => opt.UseDestinationValue())
                .ForMember(x => x.CreatedDate, opt => opt.UseDestinationValue())
                .ForMember(x => x.CreatedBy, opt => opt.UseDestinationValue())
                .ForMember(x => x.CreatedById, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(s => DateTime.UtcNow));
        }
        private string RemoveHTMLTags(string HTMLCode)
        {
            return Regex.Replace(HTMLCode, @"<[^>]*>", String.Empty);
        }
    }
}

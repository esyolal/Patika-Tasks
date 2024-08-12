using AutoMapper;
using BookStore.Entity;
using BookStore.Dto;

namespace BookStore.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre));
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();


        CreateMap<Author, AuthorDto>();
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<UpdateAuthorDto, Author>();

        CreateMap<Genre, GenreDto>();
        CreateMap<CreateGenreDto, Genre>();
        CreateMap<UpdateGenreDto, Genre>();
    }
}

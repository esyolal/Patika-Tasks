using BookStore.Dto;
using BookStore.Response;
using BookStore.Entity;
using BookStore.DbOperations;
using AutoMapper;

namespace BookStore.Services;
public class GenreService : IGenreService
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GenreService(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<List<GenreDto>> GetAllGenres()
    {
        var genres = _context.Genres.ToList();
        var genreDtos = _mapper.Map<List<GenreDto>>(genres);
        return new ApiResponse<List<GenreDto>>(genreDtos);
    }

    public ApiResponse<GenreDto> GetGenreById(int id)
    {
        var genre = _context.Genres.FirstOrDefault(g => g.Id == id);
        if (genre == null)
        {
            return new ApiResponse<GenreDto>("Genre not found.");
        }
        var genreDto = _mapper.Map<GenreDto>(genre);
        return new ApiResponse<GenreDto>(genreDto);
    }

    public ApiResponse<GenreDto> AddGenre(CreateGenreDto createGenreDto)
    {
        var genre = _mapper.Map<Genre>(createGenreDto);
        _context.Genres.Add(genre);
        _context.SaveChanges();
        var genreDto = _mapper.Map<GenreDto>(genre);
        return new ApiResponse<GenreDto>(genreDto);
    }

    public ApiResponse<GenreDto> UpdateGenre(int id, UpdateGenreDto updateGenreDto)
    {
        var genre = _context.Genres.FirstOrDefault(g => g.Id == id);
        if (genre == null)
        {
            return new ApiResponse<GenreDto>("Genre not found.");
        }
        _mapper.Map(updateGenreDto, genre);
        _context.SaveChanges();
        var genreDto = _mapper.Map<GenreDto>(genre);
        return new ApiResponse<GenreDto>(genreDto);
    }

    public ApiResponse<string> DeleteGenre(int id)
    {
        var genre = _context.Genres.FirstOrDefault(g => g.Id == id);
        if (genre == null)
        {
            return new ApiResponse<string>("Genre not found.");
        }
        _context.Genres.Remove(genre);
        _context.SaveChanges();
        return new ApiResponse<string>("Genre deleted successfully.");
    }
}
using BookStore.Dto;
using BookStore.Response;

namespace BookStore.Services;

public interface IGenreService
{
    ApiResponse<List<GenreDto>> GetAllGenres();
    ApiResponse<GenreDto> GetGenreById(int id);
    ApiResponse<GenreDto> AddGenre(CreateGenreDto createGenreDto);
    ApiResponse<GenreDto> UpdateGenre(int id, UpdateGenreDto updateGenreDto);
    ApiResponse<string> DeleteGenre(int id);
}
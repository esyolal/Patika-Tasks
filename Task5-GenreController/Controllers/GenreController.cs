using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.Response;
using BookStore.Dto;
namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public ApiResponse<List<GenreDto>> GetAllGenres()
        {
            return _genreService.GetAllGenres();
        }

        [HttpGet("{id}")]
        public ApiResponse<GenreDto> GetGenreById(int id)
        {
            return _genreService.GetGenreById(id);
        }

        [HttpPost]
        public ApiResponse<GenreDto> AddGenre([FromBody] CreateGenreDto createGenreDto)
        {
            return _genreService.AddGenre(createGenreDto);
        }

        [HttpPut("{id}")]
        public ApiResponse<GenreDto> UpdateGenre(int id, [FromBody] UpdateGenreDto updateGenreDto)
        {
            return _genreService.UpdateGenre(id, updateGenreDto);
        }

        [HttpDelete("{id}")]
        public ApiResponse<string> DeleteGenre(int id)
        {
            return _genreService.DeleteGenre(id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.Response;
using BookStore.Dto;
namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public ApiResponse<List<AuthorDto>> GetAllAuthors()
        {
            return _authorService.GetAllAuthors();
        }

        [HttpGet("{id}")]
        public ApiResponse<AuthorDto> GetAuthorById(int id)
        {
            return _authorService.GetAuthorById(id);
        }

        [HttpPost]
        public ApiResponse<AuthorDto> AddAuthor([FromBody] CreateAuthorDto createAuthorDto)
        {
            return _authorService.AddAuthor(createAuthorDto);
        }

        [HttpPut("{id}")]
        public ApiResponse<AuthorDto> UpdateAuthor(int id, [FromBody] UpdateAuthorDto updateAuthorDto)
        {
            return _authorService.UpdateAuthor(id, updateAuthorDto);
        }

        [HttpDelete("{id}")]
        public ApiResponse<string> DeleteAuthor(int id)
        {
            return _authorService.DeleteAuthor(id);
        }
    }
}

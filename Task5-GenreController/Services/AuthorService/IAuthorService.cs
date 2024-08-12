using BookStore.Dto;
using BookStore.Response;

namespace BookStore.Services;

public interface IAuthorService
{
    ApiResponse<List<AuthorDto>> GetAllAuthors();
    ApiResponse<AuthorDto> GetAuthorById(int id);
    ApiResponse<AuthorDto> AddAuthor(CreateAuthorDto createAuthorDto);
    ApiResponse<AuthorDto> UpdateAuthor(int id, UpdateAuthorDto updateAuthorDto);
    ApiResponse<string> DeleteAuthor(int id);
}
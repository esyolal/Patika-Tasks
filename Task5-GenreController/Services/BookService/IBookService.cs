using BookStore.Dto;
using BookStore.Response;

namespace BookStore.Services;

public interface IBookService
{
    ApiResponse<List<BookDto>> GetAllBooks();
    ApiResponse<BookDto> GetBookById(int id);
    ApiResponse<BookDto> AddBook(CreateBookDto createBookDto);
    ApiResponse<BookDto> UpdateBook(int id, UpdateBookDto updateBookDto);
    ApiResponse<string> DeleteBook(int id);
}
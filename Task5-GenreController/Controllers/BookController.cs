using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.Response;
using BookStore.Dto;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ApiResponse<List<BookDto>> GetAllBooks()
        {
            return _bookService.GetAllBooks();
        }

        [HttpGet("{id}")]
        public ApiResponse<BookDto> GetBookById(int id)
        {
            return _bookService.GetBookById(id);
        }

        [HttpPost]
        public ApiResponse<BookDto> AddBook([FromBody] CreateBookDto createBookDto)
        {
            return _bookService.AddBook(createBookDto);
        }

        [HttpPut("{id}")]
        public ApiResponse<BookDto> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            return _bookService.UpdateBook(id, updateBookDto);
        }

        [HttpDelete("{id}")]
        public ApiResponse<string> DeleteBook(int id)
        {
            return _bookService.DeleteBook(id);
        }
    }
}

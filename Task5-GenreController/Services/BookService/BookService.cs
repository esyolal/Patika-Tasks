using AutoMapper;
using BookStore.Response;
using BookStore.Entity;
using BookStore.Dto;
using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;
namespace BookStore.Services;

public class BookService : IBookService
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BookService(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<List<BookDto>> GetAllBooks()
    {
        var books = _context.Books.Include(b => b.Author).Include(b => b.Genre).ToList();
        var bookDtos = _mapper.Map<List<BookDto>>(books);
        return new ApiResponse<List<BookDto>>(bookDtos);
    }

    public ApiResponse<BookDto> GetBookById(int id)
    {
        var book = _context.Books.Include(b => b.Author).Include(b => b.Genre).FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return new ApiResponse<BookDto>("Book not found.");
        }
        var bookDto = _mapper.Map<BookDto>(book);
        return new ApiResponse<BookDto>(bookDto);
    }

    public ApiResponse<BookDto> AddBook(CreateBookDto createBookDto)
    {
        var book = _mapper.Map<Book>(createBookDto);
        _context.Books.Add(book);
        _context.SaveChanges();
        var bookDto = _mapper.Map<BookDto>(book);
        return new ApiResponse<BookDto>(bookDto);
    }

    public ApiResponse<BookDto> UpdateBook(int id, UpdateBookDto updateBookDto)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return new ApiResponse<BookDto>("Book not found.");
        }
        _mapper.Map(updateBookDto, book);
        _context.SaveChanges();
        var bookDto = _mapper.Map<BookDto>(book);
        return new ApiResponse<BookDto>(bookDto);
    }

    public ApiResponse<string> DeleteBook(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return new ApiResponse<string>("Book not found.");
        }
        _context.Books.Remove(book);
        _context.SaveChanges();
        return new ApiResponse<string>("Book deleted successfully.");
    }
}
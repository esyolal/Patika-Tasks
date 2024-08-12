using AutoMapper;
using BookStore.Response;
using BookStore.Entity;
using BookStore.Dto;
using BookStore.DbOperations;
namespace BookStore.Services;
public class AuthorService : IAuthorService
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public AuthorService(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ApiResponse<List<AuthorDto>> GetAllAuthors()
    {
        var authors = _context.Authors.ToList();
        var authorDtos = _mapper.Map<List<AuthorDto>>(authors);
        return new ApiResponse<List<AuthorDto>>(authorDtos);
    }

    public ApiResponse<AuthorDto> GetAuthorById(int id)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null)
        {
            return new ApiResponse<AuthorDto>("Author not found.");
        }
        var authorDto = _mapper.Map<AuthorDto>(author);
        return new ApiResponse<AuthorDto>(authorDto);
    }

    public ApiResponse<AuthorDto> AddAuthor(CreateAuthorDto createAuthorDto)
    {
        var author = _mapper.Map<Author>(createAuthorDto);
        _context.Authors.Add(author);
        _context.SaveChanges();
        var authorDto = _mapper.Map<AuthorDto>(author);
        return new ApiResponse<AuthorDto>(authorDto);
    }

    public ApiResponse<AuthorDto> UpdateAuthor(int id, UpdateAuthorDto updateAuthorDto)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null)
        {
            return new ApiResponse<AuthorDto>("Author not found.");
        }
        _mapper.Map(updateAuthorDto, author);
        _context.SaveChanges();
        var authorDto = _mapper.Map<AuthorDto>(author);
        return new ApiResponse<AuthorDto>(authorDto);
    }

    public ApiResponse<string> DeleteAuthor(int id)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null)
        {
            return new ApiResponse<string>("Author not found.");
        }
        if (_context.Books.Any(b => b.AuthorId == id))
        {
            return new ApiResponse<string>("Author has published books. Delete the books first.");
        }
        _context.Authors.Remove(author);
        _context.SaveChanges();
        return new ApiResponse<string>("Author deleted successfully.");
    }
}
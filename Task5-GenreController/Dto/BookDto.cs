namespace BookStore.Dto;
public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public AuthorDto Author { get; set; }
    public GenreDto Genre { get; set; }
}

public class CreateBookDto
{
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
}

public class UpdateBookDto
{
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
}

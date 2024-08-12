namespace BookStore.Dto;
public class GenreDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CreateGenreDto
{
    public string Name { get; set; }
}

public class UpdateGenreDto
{
    public string Name { get; set; }
}

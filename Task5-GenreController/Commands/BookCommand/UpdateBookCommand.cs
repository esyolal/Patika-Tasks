
using BookStore.DbOperations;

namespace BookStore.Commands.BookCommand;

public class UpdateBookCommand
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }

    public UpdateBookCommand(int id, string title, string author, string genre, decimal price)
    {
        Id = id;
        Title = title;
        Author = author;
        Genre = genre;
        Price = price;
    }
}

public class UpdateBookCommandHandler
{
    private readonly BookStoreDbContext _dbContext;

    public UpdateBookCommandHandler(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle(UpdateBookCommand command)
    {
        var book = _dbContext.Books.Find(command.Id);
        if (book == null) throw new InvalidOperationException("Book not found.");

        book.Title = command.Title;
        book.Author = command.Author;
        book.Genre = command.Genre;
        book.Price = command.Price;
        _dbContext.SaveChanges();
    }
}

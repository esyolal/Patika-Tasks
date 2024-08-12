using BookStore.DbOperations;

namespace BookStore.Commands.BookCommand;
public class DeleteBookCommand
{
    public int Id { get; set; }

    public DeleteBookCommand(int id)
    {
        Id = id;
    }
}

public class DeleteBookCommandHandler
{
    private readonly BookStoreDbContext _dbContext;

    public DeleteBookCommandHandler(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle(DeleteBookCommand command)
    {
        var book = _dbContext.Books.Find(command.Id);
        if (book == null) throw new InvalidOperationException("Book not found.");
        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }
}

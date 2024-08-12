using BookStore.Commands.BookCommand;
using BookStore.DbOperations;
using BookStore.Entity;
using Moq;
using Xunit;

public class UpdateBookCommandTest
{
    private readonly Mock<BookStoreDbContext> _mockContext;
    private readonly UpdateBookCommandHandler _handler;

    public UpdateBookCommandTest()
    {
        _mockContext = new Mock<BookStoreDbContext>();
        _handler = new UpdateBookCommandHandler(_mockContext.Object);
    }

    [Fact]
    public void Handle_ShouldUpdateBook_WhenBookExists()
    {
        // Arrange
        var book = new Book { Id = 1 };
        _mockContext.Setup(c => c.Books.Find(1)).Returns(book);

        // Act
        _handler.Handle(new UpdateBookCommand(1, "New Title", "New Author", "New Genre", 20.0m));

        // Assert
        Assert.Equal("New Title", book.Title);
        Assert.Equal("New Author", book.Author);
        Assert.Equal("New Genre", book.Genre);
        Assert.Equal(20.0m, book.Price);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Handle_ShouldThrowException_WhenBookDoesNotExist()
    {
        // Arrange
        _mockContext.Setup(c => c.Books.Find(1)).Returns((Book)null);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _handler.Handle(new UpdateBookCommand(1, "Title", "Author", "Genre", 20.0m)));
    }
}

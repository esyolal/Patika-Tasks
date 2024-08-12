using BookStore.Commands.BookCommand;
using BookStore.DbOperations;
using BookStore.Entity;
using Moq;
using Xunit;

namespace BookStore.Commands.Tests;
public class DeleteBookCommandTest
{
    private readonly Mock<BookStoreDbContext> _mockContext;
    private readonly DeleteBookCommandHandler _handler;

    public DeleteBookCommandTest()
    {
        _mockContext = new Mock<BookStoreDbContext>();
        _handler = new DeleteBookCommandHandler(_mockContext.Object);
    }

    [Fact]
    public void Handle_ShouldDeleteBook_WhenBookExists()
    {
        // Arrange
        var book = new Book { Id = 1 };
        _mockContext.Setup(c => c.Books.Find(1)).Returns(book);

        // Act
        _handler.Handle(new DeleteBookCommand(1));

        // Assert
        _mockContext.Verify(c => c.Books.Remove(book), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Handle_ShouldThrowException_WhenBookDoesNotExist()
    {
        // Arrange
        _mockContext.Setup(c => c.Books.Find(1)).Returns((Book)null);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _handler.Handle(new DeleteBookCommand(1)));
    }
}

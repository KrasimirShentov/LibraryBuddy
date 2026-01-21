using LibraryBuddy.Domain.Entities;

namespace LibraryBuddy.ViewModels.Books;

public class BookListVm
{
    public BookFilterVm Filter { get; set; } = new();
    public List<Book> Items { get; set; } = new();
}

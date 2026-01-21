using LibraryBuddy.Domain.Enums;

namespace LibraryBuddy.ViewModels.Books;

public class BookFilterVm
{
    public string? Q { get; set; }
    public string? Genre { get; set; }
    public BookStatus? Status { get; set; }

    public List<string> Genres { get; set; } = new();
}

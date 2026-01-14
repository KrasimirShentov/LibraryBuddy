using LibraryBuddy.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LibraryBuddy.Domain.Entities;

public class Book
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(120)]
    public string Author { get; set; } = string.Empty;

    [Required, StringLength(60)]
    public string Genre { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    public BookStatus Status { get; set; } = BookStatus.Available;

    public List<Loan> Loans { get; set; } = new();
}

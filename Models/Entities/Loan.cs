using System.ComponentModel.DataAnnotations;

namespace LibraryBuddy.Domain.Entities;

public class Loan
{
    public int Id { get; set; }

    [Required]
    public int BookId { get; set; }

    public Book? Book { get; set; }

    [Required, StringLength(120)]
    public string BorrowerName { get; set; } = string.Empty;

    [Required]
    public DateOnly LoanedOn { get; set; }

    public DateOnly? ReturnedOn { get; set; }

    public bool IsActive => ReturnedOn is null;
}

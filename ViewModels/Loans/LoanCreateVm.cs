using System.ComponentModel.DataAnnotations;

namespace LibraryBuddy.ViewModels.Loans;

public class LoanCreateVm
{
    public int BookId { get; set; }

    public string BookTitle { get; set; } = "";

    [Required, StringLength(120)]
    public string BorrowerName { get; set; } = "";

    [Required]
    [DataType(DataType.Date)]
    public DateOnly LoanedOn { get; set; } = DateOnly.FromDateTime(DateTime.Today);
}

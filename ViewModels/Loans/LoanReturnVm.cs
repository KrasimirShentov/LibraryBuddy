using System.ComponentModel.DataAnnotations;

namespace LibraryBuddy.ViewModels.Loans;

public class LoanReturnVm
{
    public int BookId { get; set; }
    public string BookTitle { get; set; } = "";

    [Required]
    [DataType(DataType.Date)]
    public DateOnly ReturnedOn { get; set; } = DateOnly.FromDateTime(DateTime.Today);
}

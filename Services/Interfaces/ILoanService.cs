using LibraryBuddy.Domain.Entities;

namespace LibraryBuddy.Services.Interfaces;

public interface ILoanService
{
    Task<Loan?> GetActiveLoanAsync(int bookId);
    Task<IReadOnlyList<Loan>> GetActiveLoansAsync();

    Task LoanBookAsync(int bookId, string borrowerName, DateOnly loanedOn);
    Task ReturnBookAsync(int bookId, DateOnly returnedOn);
}

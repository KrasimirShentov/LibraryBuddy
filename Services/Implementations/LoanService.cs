using LibraryBuddy.Data;
using LibraryBuddy.Domain.Entities;
using LibraryBuddy.Domain.Enums;
using LibraryBuddy.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryBuddy.Services.Implementations;

public class LoanService : ILoanService
{
    private readonly LibraryBuddyDbContext _db;

    public LoanService(LibraryBuddyDbContext db) => _db = db;

    public Task<Loan?> GetActiveLoanAsync(int bookId) =>
        _db.Loans.AsNoTracking().FirstOrDefaultAsync(l => l.BookId == bookId && l.ReturnedOn == null);

    public async Task<IReadOnlyList<Loan>> GetActiveLoansAsync()
    {
        return await _db.Loans.AsNoTracking()
            .Include(l => l.Book)
            .Where(l => l.ReturnedOn == null)
            .OrderBy(l => l.LoanedOn)
            .ToListAsync();
    }

    public async Task LoanBookAsync(int bookId, string borrowerName, DateOnly loanedOn)
    {
        borrowerName = borrowerName?.Trim() ?? "";
        if (string.IsNullOrWhiteSpace(borrowerName))
            throw new InvalidOperationException("Borrower name is required.");

        // Transaction to keep Book.Status and Loan in sync
        await using var tx = await _db.Database.BeginTransactionAsync();

        var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == bookId);
        if (book is null)
            throw new InvalidOperationException("Book not found.");

        if (book.Status == BookStatus.Loaned)
            throw new InvalidOperationException("This book is already loaned.");

        // Safety check (even with unique filtered index)
        var activeLoanExists = await _db.Loans.AnyAsync(l => l.BookId == bookId && l.ReturnedOn == null);
        if (activeLoanExists)
            throw new InvalidOperationException("This book already has an active loan.");

        var loan = new Loan
        {
            BookId = bookId,
            BorrowerName = borrowerName,
            LoanedOn = loanedOn,
            ReturnedOn = null
        };

        _db.Loans.Add(loan);
        book.Status = BookStatus.Loaned;

        await _db.SaveChangesAsync();
        await tx.CommitAsync();
    }

    public async Task ReturnBookAsync(int bookId, DateOnly returnedOn)
    {
        await using var tx = await _db.Database.BeginTransactionAsync();

        var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == bookId);
        if (book is null)
            throw new InvalidOperationException("Book not found.");

        var loan = await _db.Loans
            .Where(l => l.BookId == bookId && l.ReturnedOn == null)
            .OrderByDescending(l => l.LoanedOn)
            .FirstOrDefaultAsync();

        if (loan is null)
            throw new InvalidOperationException("This book has no active loan to return.");

        if (returnedOn < loan.LoanedOn)
            throw new InvalidOperationException("Return date cannot be earlier than loan date.");

        loan.ReturnedOn = returnedOn;
        book.Status = BookStatus.Available;

        await _db.SaveChangesAsync();
        await tx.CommitAsync();
    }
}

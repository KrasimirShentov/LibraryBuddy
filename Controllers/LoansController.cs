using LibraryBuddy.Services.Interfaces;
using LibraryBuddy.ViewModels.Loans;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBuddy.Controllers;

public class LoansController : Controller
{
    private readonly IBookService _books;
    private readonly ILoanService _loans;

    public LoansController(IBookService books, ILoanService loans)
    {
        _books = books;
        _loans = loans;
    }

    // Shows only currently loaned books
    public async Task<IActionResult> Loaned()
    {
        var activeLoans = await _loans.GetActiveLoansAsync();
        return View(activeLoans);
    }

    public async Task<IActionResult> Create(int bookId)
    {
        var book = await _books.GetByIdAsync(bookId);
        if (book is null) return NotFound();

        var vm = new LoanCreateVm
        {
            BookId = book.Id,
            BookTitle = $"{book.Title} — {book.Author}"
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LoanCreateVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        try
        {
            await _loans.LoanBookAsync(vm.BookId, vm.BorrowerName, vm.LoanedOn);
            TempData["Success"] = "Book loaned successfully.";
            return RedirectToAction("Details", "Books", new { id = vm.BookId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            // Rehydrate book title for redisplay
            var book = await _books.GetByIdAsync(vm.BookId);
            vm.BookTitle = book is null ? "" : $"{book.Title} — {book.Author}";
            return View(vm);
        }
    }

    public async Task<IActionResult> Return(int bookId)
    {
        var book = await _books.GetByIdAsync(bookId);
        if (book is null) return NotFound();

        var vm = new LoanReturnVm
        {
            BookId = book.Id,
            BookTitle = $"{book.Title} — {book.Author}"
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Return(LoanReturnVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        try
        {
            await _loans.ReturnBookAsync(vm.BookId, vm.ReturnedOn);
            TempData["Success"] = "Book returned successfully.";
            return RedirectToAction("Details", "Books", new { id = vm.BookId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            var book = await _books.GetByIdAsync(vm.BookId);
            vm.BookTitle = book is null ? "" : $"{book.Title} — {book.Author}";
            return View(vm);
        }
    }
}

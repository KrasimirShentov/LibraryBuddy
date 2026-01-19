using LibraryBuddy.Domain.Entities;
using LibraryBuddy.Domain.Enums;
using LibraryBuddy.Services.Interfaces;
using LibraryBuddy.ViewModels.Books;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBuddy.Controllers;

public class BooksController : Controller
{
    private readonly IBookService _books;
    private readonly ILoanService _loans;

    public BooksController(IBookService books, ILoanService loans)
    {
        _books = books;
        _loans = loans;
    }

    public async Task<IActionResult> Index(string? q, string? genre, BookStatus? status)
    {
        var (items, genres) = await _books.SearchAsync(q, genre, status);

        var vm = new BookListVm
        {
            Filter = new BookFilterVm
            {
                Q = q,
                Genre = genre,
                Status = status,
                Genres = genres.ToList()
            },
            Items = items.ToList()
        };

        return View(vm);
    }

    public async Task<IActionResult> Details(int id)
    {
        var book = await _books.GetByIdAsync(id);
        if (book is null) return NotFound();

        var activeLoan = book.Loans.FirstOrDefault(l => l.ReturnedOn == null);
        ViewBag.ActiveLoan = activeLoan;

        return View(book);
    }

    public IActionResult Create() => View(new BookCreateVm());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookCreateVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var book = new Book
        {
            Title = vm.Title.Trim(),
            Author = vm.Author.Trim(),
            Genre = vm.Genre.Trim(),
            Description = vm.Description?.Trim()
        };

        await _books.CreateAsync(book);
        TempData["Success"] = "Book created.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var book = await _books.GetByIdAsync(id);
        if (book is null) return NotFound();

        return View(new BookEditVm
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre,
            Description = book.Description
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BookEditVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var ok = await _books.UpdateAsync(new Book
        {
            Id = vm.Id,
            Title = vm.Title.Trim(),
            Author = vm.Author.Trim(),
            Genre = vm.Genre.Trim(),
            Description = vm.Description?.Trim()
        });

        if (!ok) return NotFound();

        TempData["Success"] = "Book updated.";
        return RedirectToAction(nameof(Details), new { id = vm.Id });
    }

    public async Task<IActionResult> Delete(int id)
    {
        var book = await _books.GetByIdAsync(id);
        if (book is null) return NotFound();
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var ok = await _books.DeleteAsync(id);
        if (!ok) return NotFound();

        TempData["Success"] = "Book deleted.";
        return RedirectToAction(nameof(Index));
    }

    // Convenience actions for loan/return (redirect to LoansController)
    public async Task<IActionResult> Loan(int id)
    {
        var book = await _books.GetByIdAsync(id);
        if (book is null) return NotFound();
        return RedirectToAction("Create", "Loans", new { bookId = id });
    }

    public async Task<IActionResult> Return(int id)
    {
        var book = await _books.GetByIdAsync(id);
        if (book is null) return NotFound();
        return RedirectToAction("Return", "Loans", new { bookId = id });
    }
}

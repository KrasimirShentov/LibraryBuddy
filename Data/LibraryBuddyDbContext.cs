using LibraryBuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class LibraryBuddyDbContext : DbContext
{
    public LibraryBuddyDbContext(DbContextOptions<LibraryBuddyDbContext> options)
        : base(options) { }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Loans)
            .WithOne(l => l.Book!)
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>().HasIndex(b => b.Title);
        modelBuilder.Entity<Book>().HasIndex(b => b.Author);
        modelBuilder.Entity<Book>().HasIndex(b => b.Genre);

        modelBuilder.Entity<Loan>()
            .HasIndex(l => l.BookId)
            .IsUnique()
            .HasFilter("[ReturnedOn] IS NULL");
    }
}

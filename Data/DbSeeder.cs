using LibraryBuddy.Domain.Entities;
using LibraryBuddy.Domain.Enums;

namespace LibraryBuddy.Data;

public static class DbSeeder
{
    public static void Seed(LibraryBuddyDbContext db)
    {
        if (db.Books.Any())
            return; // Prevent duplicate seeding

        var books = new List<Book>
        {
            // ===== Programming =====
            new() { Title="Clean Code", Author="Robert C. Martin", Genre="Programming", Description="Agile software craftsmanship.", Status=BookStatus.Available },
            new() { Title="Clean Architecture", Author="Robert C. Martin", Genre="Programming", Description="Software architecture principles.", Status=BookStatus.Available },
            new() { Title="The Pragmatic Programmer", Author="Andrew Hunt", Genre="Programming", Description="From journeyman to master.", Status=BookStatus.Available },
            new() { Title="Refactoring", Author="Martin Fowler", Genre="Programming", Description="Improving the design of existing code.", Status=BookStatus.Loaned },
            new() { Title="Design Patterns", Author="Erich Gamma", Genre="Programming", Description="Reusable OO patterns.", Status=BookStatus.Available },
            new() { Title="Domain-Driven Design", Author="Eric Evans", Genre="Programming", Description="Tackling complexity in software.", Status=BookStatus.Available },
            new() { Title="Head First Design Patterns", Author="Eric Freeman", Genre="Programming", Description="Visual guide to patterns.", Status=BookStatus.Loaned },
            new() { Title="You Don’t Know JS", Author="Kyle Simpson", Genre="Programming", Description="Deep dive into JavaScript.", Status=BookStatus.Available },
            new() { Title="Effective C#", Author="Bill Wagner", Genre="Programming", Description="C# best practices.", Status=BookStatus.Available },
            new() { Title="CLR via C#", Author="Jeffrey Richter", Genre="Programming", Description=".NET internals.", Status=BookStatus.Loaned },

            // ===== Fantasy =====
            new() { Title="The Hobbit", Author="J.R.R. Tolkien", Genre="Fantasy", Description="An unexpected journey.", Status=BookStatus.Available },
            new() { Title="The Fellowship of the Ring", Author="J.R.R. Tolkien", Genre="Fantasy", Description="LOTR Part 1.", Status=BookStatus.Loaned },
            new() { Title="The Two Towers", Author="J.R.R. Tolkien", Genre="Fantasy", Description="LOTR Part 2.", Status=BookStatus.Available },
            new() { Title="The Return of the King", Author="J.R.R. Tolkien", Genre="Fantasy", Description="LOTR Part 3.", Status=BookStatus.Available },
            new() { Title="A Game of Thrones", Author="George R.R. Martin", Genre="Fantasy", Description="Song of Ice and Fire.", Status=BookStatus.Loaned },
            new() { Title="A Clash of Kings", Author="George R.R. Martin", Genre="Fantasy", Description="Political fantasy.", Status=BookStatus.Available },
            new() { Title="The Name of the Wind", Author="Patrick Rothfuss", Genre="Fantasy", Description="Story of Kvothe.", Status=BookStatus.Available },
            new() { Title="The Way of Kings", Author="Brandon Sanderson", Genre="Fantasy", Description="Epic world-building.", Status=BookStatus.Loaned },
            new() { Title="Mistborn", Author="Brandon Sanderson", Genre="Fantasy", Description="Allomancy magic system.", Status=BookStatus.Available },
            new() { Title="The Witcher", Author="Andrzej Sapkowski", Genre="Fantasy", Description="Monster hunter saga.", Status=BookStatus.Available },

            // ===== Classics =====
            new() { Title="1984", Author="George Orwell", Genre="Classic", Description="Totalitarian dystopia.", Status=BookStatus.Available },
            new() { Title="Animal Farm", Author="George Orwell", Genre="Classic", Description="Political allegory.", Status=BookStatus.Loaned },
            new() { Title="Brave New World", Author="Aldous Huxley", Genre="Classic", Description="Future society.", Status=BookStatus.Available },
            new() { Title="Fahrenheit 451", Author="Ray Bradbury", Genre="Classic", Description="Book censorship.", Status=BookStatus.Available },
            new() { Title="To Kill a Mockingbird", Author="Harper Lee", Genre="Classic", Description="Justice and race.", Status=BookStatus.Loaned },
            new() { Title="The Great Gatsby", Author="F. Scott Fitzgerald", Genre="Classic", Description="American dream.", Status=BookStatus.Available },
            new() { Title="Moby-Dick", Author="Herman Melville", Genre="Classic", Description="Obsession and revenge.", Status=BookStatus.Available },
            new() { Title="Crime and Punishment", Author="Fyodor Dostoevsky", Genre="Classic", Description="Psychological drama.", Status=BookStatus.Loaned },
            new() { Title="War and Peace", Author="Leo Tolstoy", Genre="Classic", Description="Historical epic.", Status=BookStatus.Available },
            new() { Title="The Brothers Karamazov", Author="Fyodor Dostoevsky", Genre="Classic", Description="Philosophical novel.", Status=BookStatus.Available },

            // ===== Sci-Fi =====
            new() { Title="Dune", Author="Frank Herbert", Genre="Sci-Fi", Description="Desert planet politics.", Status=BookStatus.Available },
            new() { Title="Foundation", Author="Isaac Asimov", Genre="Sci-Fi", Description="Psychohistory.", Status=BookStatus.Loaned },
            new() { Title="Neuromancer", Author="William Gibson", Genre="Sci-Fi", Description="Cyberpunk classic.", Status=BookStatus.Available },
            new() { Title="Snow Crash", Author="Neal Stephenson", Genre="Sci-Fi", Description="Virtual reality future.", Status=BookStatus.Available },
            new() { Title="The Martian", Author="Andy Weir", Genre="Sci-Fi", Description="Survival on Mars.", Status=BookStatus.Loaned },
            new() { Title="Ender’s Game", Author="Orson Scott Card", Genre="Sci-Fi", Description="Child military genius.", Status=BookStatus.Available },
            new() { Title="Hyperion", Author="Dan Simmons", Genre="Sci-Fi", Description="Epic sci-fi saga.", Status=BookStatus.Available },
            new() { Title="Blade Runner", Author="Philip K. Dick", Genre="Sci-Fi", Description="Androids and humanity.", Status=BookStatus.Loaned },
            new() { Title="The Expanse", Author="James S. A. Corey", Genre="Sci-Fi", Description="Space opera.", Status=BookStatus.Available },
            new() { Title="Ready Player One", Author="Ernest Cline", Genre="Sci-Fi", Description="Virtual reality quest.", Status=BookStatus.Available },

            // ===== History / Non-fiction =====
            new() { Title="Sapiens", Author="Yuval Noah Harari", Genre="History", Description="Human history.", Status=BookStatus.Available },
            new() { Title="Homo Deus", Author="Yuval Noah Harari", Genre="History", Description="Future of humanity.", Status=BookStatus.Available },
            new() { Title="Guns, Germs, and Steel", Author="Jared Diamond", Genre="History", Description="Civilization development.", Status=BookStatus.Loaned },
            new() { Title="The Silk Roads", Author="Peter Frankopan", Genre="History", Description="Global history.", Status=BookStatus.Available },
            new() { Title="The Art of War", Author="Sun Tzu", Genre="History", Description="Military strategy.", Status=BookStatus.Available },

            // ===== Psychology / Self-Help =====
            new() { Title="Atomic Habits", Author="James Clear", Genre="Self-Help", Description="Habit building.", Status=BookStatus.Available },
            new() { Title="Deep Work", Author="Cal Newport", Genre="Self-Help", Description="Focused success.", Status=BookStatus.Loaned },
            new() { Title="Thinking, Fast and Slow", Author="Daniel Kahneman", Genre="Psychology", Description="Human thinking biases.", Status=BookStatus.Available },
            new() { Title="Man’s Search for Meaning", Author="Viktor Frankl", Genre="Psychology", Description="Meaning of life.", Status=BookStatus.Available },
            new() { Title="The Power of Habit", Author="Charles Duhigg", Genre="Self-Help", Description="Habit loops.", Status=BookStatus.Loaned },
        };

        db.Books.AddRange(books);
        db.SaveChanges();
    }
}

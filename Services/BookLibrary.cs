using web_api.Models;
using web_api.Interfaces;
namespace web_api.Services;

public class BookLibrary:IBookLibrary
{
    private readonly List<Book> _books;
    private int _nextId = 1;

    public BookLibrary()
    {
        _books = new List<Book>();
    }

    public void Create(Book book)
    {
        if(book == null)
        {
            throw new ArgumentNullException(nameof(book));
        }
        book.Id = _nextId++;
        _books.Add(book);
    }

    public void Delete(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if(book == null)
        {
            throw new KeyNotFoundException($"Book with Id {id} not found.");
        }
        _books.Remove(book);
    }

    public void Update(Book book)
    {
        if(book == null)
        {
            throw new ArgumentNullException(nameof(book));
        }
        var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
        if(existingBook == null)
        {
            throw new KeyNotFoundException($"Book with Id {book.Id} not found.");
        }
        _books.Remove(existingBook);
        _books.Add(book);
    }

    public IEnumerable<Book> ReadAll()
    {
        List<Book> b= new List<Book>(_books);
        return b;
    }
    public Book? Read(int id)
    {
        
        Book? b = _books.FirstOrDefault(b => b.Id == id);
        return b;
    }

  }

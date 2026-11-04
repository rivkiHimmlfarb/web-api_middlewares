using web_api.Models;

namespace web_api.Interfaces;

public interface IBookLibrary
{
    void Create(Book book);
    void Delete(int id);
    Book? Read(int id);
    IEnumerable<Book> ReadAll();
    void Update(Book book);


}




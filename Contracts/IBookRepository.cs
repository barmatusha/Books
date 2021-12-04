using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBookRepository
    {
        IList<Book> GetBooks();
        Book GetBook(int id, bool trackChanges);
        void CreateBook(Book book);
        void DeleteBook(Book book);
    }
}

using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateBook(Book book) =>
            Create(book);

        public void DeleteBook(Book book) =>
            Delete(book);

        public Book GetBook(int id, bool trackChanges) =>
            FindByCondition(e => e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public IList<Book> GetBooks()=>
            RepositoryContext.Books.Include(a => a.Authors).ToList();
    }
}

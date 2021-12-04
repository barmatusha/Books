using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IAuthorRepository _authorRepository;
        private IBookRepository _bookRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IAuthorRepository Author
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new AuthorRepository(_repositoryContext);
                return _authorRepository;
            }
        }
        public IBookRepository Book
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(_repositoryContext);
                return _bookRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}

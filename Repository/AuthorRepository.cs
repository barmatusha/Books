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
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void CreateAuthor(Author author)
        {
            Create(author);
        }
        public void DeleteAuthor(Author author) =>
            Delete(author);

        public void UpdateAuthor(Author author) => 
            Update(author);

        public Author GetAuthor(int id, bool trackChanges) =>
            FindByCondition(c => c.AuthorId.Equals(id), trackChanges)
            .SingleOrDefault();

        public IEnumerable<Author> GetAuthorBooks(int bookId, bool trackChanges) =>
            FindByCondition(x => x.AuthorId.Equals(bookId), trackChanges)
            .OrderBy(e => e.Surname);

        public IList<Author> GetAuthors() =>
            RepositoryContext.Authors.Include(a => a.Books).ToList();

    }
}

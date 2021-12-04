using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAuthorBooks(int AuthorId, bool trackChanges);
        IList<Author> GetAuthors();

        Author GetAuthor(int id, bool trackChanges);
        void CreateAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
    }
}

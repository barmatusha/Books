using System.Collections.Generic;

namespace Entities.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Surname { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public Author()
        {
            Books = new List<Book>();
        }
    }
}

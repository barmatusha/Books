using System.Collections.Generic;

namespace Entities.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public Book()
        {
            Authors = new List<Author>();
        }
    }
}

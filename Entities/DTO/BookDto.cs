using System.Collections.Generic;

namespace Entities.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public List<string> AuthorsList { get; set; }
        public BookDto()
        {
            AuthorsList = new List<string>();
        }
        
    }
}

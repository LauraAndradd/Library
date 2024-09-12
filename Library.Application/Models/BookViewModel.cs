using Library.Core.Entities;

namespace Library.Application.Models
{
    public class BookViewModel
    {
        public BookViewModel(int bookId, string title, string author, string isbn, int publicationYear)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
        }

        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }

        public static BookViewModel FromEntity(Book book)
        {
            return new BookViewModel(book.Id, book.Title, book.Author, book.ISBN, book.PublicationYear);
        }
    }
}

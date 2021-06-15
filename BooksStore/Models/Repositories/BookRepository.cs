using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models.Repositories
{
    public class BookRepository : IBookRepository<Book>
    {
        List<Book> books;

        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book { id=1,description="No Description",title="C# programming" , Auther= new Auther{ id=2}
                ,imageUrl="csharp.jpg"},
                new Book { id=2,description="nothing ",title="java programming", Auther= new Auther(),imageUrl="java.jpg" },
                new Book { id=3,description="No data",title="python programming",Auther= new Auther() ,imageUrl="python.jpg" }

            };
        }
        public void add(Book entity)
        {
            entity.id = books.Max(o => o.id)+1;
            books.Add(entity);
        }

        public void delete(int id)
        {
            var book = find(id);
            books.Remove(book);
        }

        public Book find(int id)
        {
            var book = books.SingleOrDefault(o => o.id == id);
            return book; 
        }

        public IList<Book> list()
        {
            return books;
        }

        public IList<Book> Search(string term)
        {
            return books.Where(o => o.title.Contains(term) || o.description.Contains(term) || o.Auther.fullName.Contains(term)).ToList();
           
        }

        public void update(int id ,Book entity )
        {
            var book = find(id);
            book.title = entity.title;
            book.Auther = entity.Auther;
            book.description = entity.description;
            book.imageUrl = entity.imageUrl;
        }

      
    }
}

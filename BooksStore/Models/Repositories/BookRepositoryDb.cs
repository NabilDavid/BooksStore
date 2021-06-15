using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models.Repositories
{
    public class BookRepositoryDb : IBookRepository<Book>
    {
        public readonly BookStoreDbContext db;

        public BookRepositoryDb( BookStoreDbContext bookStoreDbContext)
        {
            this.db = bookStoreDbContext;
        }
        public void add(Book entity)
        {
           
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            var book = find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book find(int id)
        {
            var book = db.Books.Include(o=>o.Auther).SingleOrDefault(o => o.id == id);
            return book;
        }

        public IList<Book> list()
        {
            return db.Books.Include(o=>o.Auther).ToList();
        }

        public IList<Book> Search(string term)
        {
            var books = db.Books.Include(a => a.Auther)
               .Where(b => b.description.Contains(term)
                        || b.title.Contains(term)
                        || b.Auther.fullName.Contains(term)).ToList();
            return books;
        }

        public void update(int id, Book entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }

      
    }
}

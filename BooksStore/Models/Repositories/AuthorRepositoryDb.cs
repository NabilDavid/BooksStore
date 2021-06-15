using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models.Repositories
{
    public class AuthorRepositoryDb :IBookRepository<Auther>
    {
        private readonly BookStoreDbContext db;

        public AuthorRepositoryDb(BookStoreDbContext BookStoreDbContext)
        {
            db = BookStoreDbContext;
        }
        public void add(Auther entity)
        {
         
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            var auther = find(id);
            db.Authors.Remove(auther);
            db.SaveChanges();
        }

        public Auther find(int id)
        {
            var auther = db.Authors.FirstOrDefault(o => o.id == id);
            return auther;

        }

        public IList<Auther> list()
        {
            return db.Authors.ToList();
        }

        public IList<Auther> Search(string term)
        {
           return db.Authors.Where(o => o.fullName.Contains(term)).ToList();
        }

        public void update(int id, Auther entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }

    }
}

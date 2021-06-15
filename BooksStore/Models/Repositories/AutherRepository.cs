using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models.Repositories
{
    public class AutherRepository : IBookRepository<Auther>
    {
        IList<Auther> authers;


        public AutherRepository()
        {
            authers = new List<Auther>()
            {
                new Auther{ id=1,fullName="Nabil Dawod" },
                new Auther{ id=2,fullName="Mina Dawod" },
                new Auther{ id=3,fullName="Tahany Dawod"}


            };
        }

        public void add(Auther entity)
        {
            entity.id = authers.Max(o => o.id) + 1;
            authers.Add(entity);
        }

        public void delete(int id)
        {
            var auther = find(id);
             authers.Remove(auther);
        }

        public Auther find(int id)
        {
            var auther = authers.FirstOrDefault(o => o.id == id);
            return auther;

        }

        public IList<Auther> list()
        {
            return authers;
        }

        public IList<Auther> Search(string term)
        {
            return authers.Where(o => o.fullName.Contains(term)).ToList();
        }

        public void update(int id, Auther entity)
        {
            var auther = find(id);
            auther.fullName = entity.fullName;
        }
    }
}

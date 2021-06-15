using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models.Repositories
{
    public interface IBookRepository<TEntity>
    {
        IList<TEntity> list();

        TEntity find(int id);
        void add(TEntity entity);
        void update(int id ,TEntity entity);
        void delete(int id);
        IList<TEntity> Search(string term);

    }
}

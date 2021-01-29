using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVoteSystem.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Insert(T val);
        T Remove(T val);
        T Update(T val);
        Task<IList<T>> GetAll();
        IQueryable<T> Queryable();
        Task SaveChangesAsync();
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVoteSystem.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Insert(T val);
        Task<T> Remove(T val);
        Task<T> Update(T val);
        Task<IList<T>> GetAll();
        IQueryable<T> Queryable();
    }
}
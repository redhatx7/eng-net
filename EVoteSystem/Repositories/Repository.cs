using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVoteSystem.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly EVoteDbContext _context;
        public Repository(EVoteDbContext context)
        {
            _context = context;
        }
        
        public async Task<T> Insert(T val)
        {
            if (val == null)
            {
                throw new ArgumentNullException($"Value is null while inserting {typeof(T)}");
            }
            await _context.Set<T>().AddAsync(val);
            await _context.SaveChangesAsync();
            return val;
        }

        public async Task<T> Remove(T val)
        {
            if (val == null)
            {
                throw new ArgumentNullException($"Value is null while removing {typeof(T)}");
            }
            _context.Set<T>().Remove(val);
            await _context.SaveChangesAsync();
            return val;
        }

        public async Task<T> Update(T val)
        {
            if (val == null)
            {
                throw new ArgumentNullException($"Value is null while updating {typeof(T)}");
            }

            _context.Set<T>().Update(val);
            await _context.SaveChangesAsync();
            return val;
        }

        public async Task<IList<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public IQueryable<T> Queryable()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
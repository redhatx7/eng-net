using System;
using System.Threading.Tasks;
using EVoteSystem.DatabaseContext;
using EVoteSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Repositories
{
    public class StudentRepository : Repository<Student>, IRepository<Student>
    {
        public StudentRepository(EVoteDbContext context) : base(context)
        {
        }

        public async Task<Student> FindStudentByIdAsync(int sid)
        {
            if (sid < 0)
            {
                throw new Exception("SID most be greater than zero");
            }

            return await _context.Set<Student>().SingleOrDefaultAsync(t => t.Id == sid);
        }

        public async Task<Student> FindStudentByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty((username)))
                throw new ArgumentNullException($"Username is null {typeof(Student)}");
            return await _context.Set<Student>().SingleOrDefaultAsync(t => t.UserName == username);
        }
        

    }
}
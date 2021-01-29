using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVoteSystem.DatabaseContext;
using EVoteSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EVoteSystem.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(EVoteDbContext context) : base(context)
        {
        }

        public async Task<Student> FindStudentByIdAsync(int? sid)
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

        public async Task<IList<Student>> NotCandidateStudents(int? id)
        {
            return await _context.Set<Student>().Include(x => x.Candidates)
                .ThenInclude(x => x.Session)
                .Where(x => x.Candidates.Where(x => x.Session.VoteSessionId == id).SingleOrDefault() == null)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Student> FindStudentByNationalId(string nationalId)
        {
            return await _context.Set<Student>().SingleOrDefaultAsync(x => x.NationalCode == nationalId);
        }
        
        

    }
}
using System;
using System.Threading.Tasks;
using EVoteSystem.DatabaseContext;
using EVoteSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Repositories
{
    public class CandidateRepository : Repository<Candidate>, IRepository<Candidate>
    {
        public CandidateRepository(EVoteDbContext context) : base(context)
        {
        }

        public async Task<Candidate> FindCandidateById(int cid)
        {
            if (cid < 0)
                throw new ArgumentOutOfRangeException("Candidate Id most be greater than zero");
            return await _context.Set<Candidate>().SingleOrDefaultAsync(t => t.CandidateId == cid);
        }

        public async Task<Candidate> FindByStudentId(int sid)
        {
            if (sid < 0)
                throw new ArgumentOutOfRangeException("Student Id most be greater than zero");
            return await _context.Set<Candidate>().SingleOrDefaultAsync(t => t.Student.Id == sid);
        }
    }
}
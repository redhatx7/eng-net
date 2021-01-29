using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVoteSystem.DatabaseContext;
using EVoteSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Repositories
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(EVoteDbContext context) : base(context)
        {
        }

        public async Task<Candidate> FindCandidateById(int? cid)
        {
            if (cid < 0)
                throw new ArgumentOutOfRangeException("Candidate Id must be greater than zero");
            return await _context.Set<Candidate>().Include(x => x.Student).SingleOrDefaultAsync(t => t.CandidateId == cid);
        }

        public async Task<Candidate> FindCandidateByStudentId(int? sid)
        {
            if (sid < 0)
                throw new ArgumentOutOfRangeException("Student Id must be greater than zero");
            return await _context.Set<Candidate>().Include(x => x.Student).SingleOrDefaultAsync(t => t.Student.Id == sid);
        }

        public async Task<IList<Candidate>> FindCandidatesInSessionAsync(int? sessionId)
        {
            if (sessionId < 0)
                throw new ArgumentOutOfRangeException("Session Id must be greater than zero");
            return await _context.Set<Candidate>().Include(x => x.Session)
                .Include(x => x.Student).Where(x => x.Session.VoteSessionId == sessionId)
                .AsNoTracking().ToListAsync();
        }
    }
}
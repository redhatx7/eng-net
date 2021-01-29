using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVoteSystem.DatabaseContext;
using EVoteSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Repositories
{
    public class VoteRepository : Repository<Vote>, IVoteRepository
    {
        public VoteRepository(EVoteDbContext context) : base(context)
        {
        }

        public async Task<IList<Vote>> GetStudentVotes(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException($"Username is null while finding votes{typeof(Vote)}");
            return await _context.Set<Vote>().Where(t => t.FromStudent.UserName == username).ToListAsync();
        }

        public async Task<IList<Vote>> GetCandidateVotes(int cid)
        {
            if (cid < 0)
                throw new ArgumentOutOfRangeException($"Candidate Id must be greater than zero {typeof(Vote)}");
            return await _context.Set<Vote>().Where(t => t.ToCandidate.CandidateId == cid).ToListAsync();
        }

        public async Task<Vote> GetVoteById(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException($"Vote Id must be greater than zero {typeof(Vote)}");
            return await _context.Set<Vote>().SingleOrDefaultAsync(t => t.VoteId == id);
        }

        public async Task<IList<Vote>> GetStudentVoteByStudentIdAsync(int? id, int sessionId)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException($"Student ID Id must be greater than zero {typeof(Vote)}");
            return await _context.Set<Vote>().Where(t => t.FromStudent.Id == id && t.Session.VoteSessionId == sessionId).ToListAsync();
        }
    }
}
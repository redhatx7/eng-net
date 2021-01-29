using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVoteSystem.DatabaseContext;
using EVoteSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Repositories
{
    public class SessionRepository : Repository<VoteSession>, ISessionRepository
    {
        public SessionRepository(EVoteDbContext context) : base(context)
        {
        }

        public async Task<IList<VoteSession>> GetActiveSessions()
        {
            return await _context.VoteSessions.Where(x => x.IsActive == true)
                .ToListAsync();
        }

        public async Task<VoteSession> FindSessionById(int? id)
        {
            var session = await _context.Set<VoteSession>().Include(x => x.Candidates).
                Include(x => x.SessionVotes).SingleOrDefaultAsync(t => t.VoteSessionId == id);
            return session;
        }
    }
}
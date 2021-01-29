using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVoteSystem.Models;
using EVoteSystem.Repositories;
using EVoteSystem.Services;
using EVoteSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Controllers
{
   
    [Authorize(Roles = ValidRoles.Student)]
    public class CastVoteController : Controller
    {
        private readonly IVoteRepository _voteRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly  UserManager<Student> _studentUserManager;
        private readonly ISessionRepository _sessionRepository;

        public CastVoteController(IVoteRepository voteRepository,
            ICandidateRepository candidateRepository,
            UserManager<Student> studentUserManager,
            ISessionRepository sessionRepository)
        {
            _voteRepository = voteRepository;
            _candidateRepository = candidateRepository;
            _studentUserManager = studentUserManager;
            _sessionRepository = sessionRepository;
        }
        
        [HttpGet("[controller]/[action]/{sessionId:int}/{candidateId:int}")]
        public async Task<IActionResult> Cast(int sessionId,int candidateId)
        {
            var user = await _studentUserManager.GetUserAsync(HttpContext.User);
            var userVotedInSession = await _voteRepository.GetStudentVoteByStudentIdAsync(user.Id, sessionId);
            if (userVotedInSession.Count > 5)
            {
                return RedirectToAction("VoteCountLimit");
            }
            var candidate = await _candidateRepository.FindCandidateById(candidateId);
            return View(candidate);
        }

        [HttpPost("[controller]/[action]/{sessionId:int}/{candidateId:int}"),ActionName("Cast")]
        public async Task<IActionResult> CastConfirm(int sessionId,int candidateId )
        {
            if (ModelState.IsValid)
            {
                var user = await _studentUserManager.GetUserAsync(HttpContext.User);
                var session = await _sessionRepository.FindSessionById(sessionId);
                var candidate = await _candidateRepository.FindCandidateById(candidateId);
                var vote = new Vote()
                {
                    Session = session,
                    FromStudent = user,
                    ToCandidate = candidate
                };
                await _voteRepository.Insert(vote);
                await _voteRepository.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            var openSession = await _sessionRepository.GetActiveSessions();
            return View(openSession);
        }

        [HttpGet]
        public async Task<IActionResult> CandidateList(int id)
        {
            var candidate = await _candidateRepository.FindCandidatesInSessionAsync(id);
            return View(candidate);
        }

        public async Task<IActionResult> GetVotedPeople(int id)
        {
            var user = await _studentUserManager.GetUserAsync(HttpContext.User);
            var voted = await _voteRepository.GetStudentVoteByStudentIdAsync(user.Id, id);
            var session = await _sessionRepository.FindSessionById(id);
            return View(new Tuple<IList<Vote>,VoteSession>(voted, session));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveVote(int id)
        {
            var query = _voteRepository.Queryable();
            var vote = await query.Include(x => x.ToCandidate).ThenInclude(x => x.Student).SingleOrDefaultAsync(x => x.VoteId == id);
            return View(vote);
        }

        [HttpPost, ActionName("RemoveVote")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveVoteConfirm(int id)
        {
            var vote = await _voteRepository.GetVoteById(id);
            if (vote != null)
            {
                _voteRepository.Remove(vote);
                await _voteRepository.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public IActionResult VoteCountLimit()
        {
            return View();
        }

    }
}
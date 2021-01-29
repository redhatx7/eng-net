using System;
using System.Linq;
using System.Threading.Tasks;
using EVoteSystem.Models;
using EVoteSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Controllers
{
    [Authorize]
    public class VoteSessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public VoteSessionController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var session = await _sessionRepository.GetAll();
            return View(session);
        }

        [HttpGet]
        public IActionResult CreateSession()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSession(VoteSession session)
        {
            if (ModelState.IsValid)
            { 
                session.IsActive = true;
                await _sessionRepository.Insert(session);
               await _sessionRepository.SaveChangesAsync();
               return RedirectToAction("Index");
            }
            ModelState.AddModelError(null, "Please control inputs");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> CloseSession(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            var session = await _sessionRepository.FindSessionById(id);
            if (session == null)
                return RedirectToAction("Index");
            return View(session);
        }

        [HttpPost, ActionName("CloseSession")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseSessionConfirm(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            var session = await _sessionRepository.FindSessionById(id);
            if (session != null)
            {
                session.IsActive = false;
                _sessionRepository.Update(session);
                await _sessionRepository.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowResult(int? id)
        {
            var voteSession = await _sessionRepository.FindSessionById(id);
            var voteCount = voteSession.SessionVotes.Count;
            return null;
        }


        
        
        
        
    }
}
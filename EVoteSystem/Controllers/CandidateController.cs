using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EVoteSystem.Models;
using EVoteSystem.Repositories;
using EVoteSystem.Services;
using EVoteSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Controllers
{
    [Authorize(Roles =  ValidRoles.Candidate + "," +  ValidRoles.Deputy + "," + ValidRoles.HeadMaster )]
    public class CandidateController : Controller
    {
        private readonly UserManager<Student> _studentUserManager;
        private readonly ISessionRepository _sessionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICandidateRepository _candidateRepository;

        public CandidateController(UserManager<Student> studentUserManager,
            ISessionRepository sessionRepository,
            IStudentRepository studentRepository,
            ICandidateRepository candidateRepository)
        {
            _studentUserManager = studentUserManager;
            _sessionRepository = sessionRepository;
            _studentRepository = studentRepository;
            _candidateRepository = candidateRepository;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "VoteSession");
            }
            var query = _sessionRepository.Queryable();
            var candidates = await query.Include(x => x.Candidates)
                .ThenInclude(x => x.Student).Select(x => x.Candidates).SingleOrDefaultAsync();
            return View(candidates);
        }

        [Authorize(Roles = ValidRoles.Deputy + "," + ValidRoles.HeadMaster)]
        [HttpGet]
        public async Task<IActionResult> AddCandidate()
        {
            var sessions = await _sessionRepository.GetActiveSessions();
            return View("SelectSession", sessions);
        }
        
        [Authorize(Roles = ValidRoles.Deputy + "," + ValidRoles.HeadMaster)]
        [HttpGet("[controller]/[action]/{studentId:int}/{sessionId:int}")]
        public async Task<IActionResult> AddCandidate(int studentId, int sessionId)
        {
            var student = await _studentRepository.FindStudentByIdAsync(studentId);
            return View(student);
        }

        [Authorize(Roles = ValidRoles.Deputy + "," + ValidRoles.HeadMaster)]
        [HttpPost("[controller]/[action]/{studentId:int}/{sessionId:int}"), ActionName("AddCandidate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCandidateConfirm(int studentId, int sessionId)
        {
            if (ModelState.IsValid)
            {
                var session = await _sessionRepository.FindSessionById(sessionId);
                var student = await _studentRepository.FindStudentByIdAsync(studentId);
                if (session == null || student == null || !session.IsActive)
                {
                    ModelState.AddModelError("","خطا، ورودی ها را کنترل کنید.");
                    return View();
                }
                var candidate = new Candidate()
                {
                    Student = student,
                    Session = session,
                };
                await _candidateRepository.Insert(candidate);
                await _studentUserManager.AddToRoleAsync(student, ValidRoles.Candidate);
                var currentClaim = (await _studentUserManager.GetClaimsAsync(student)).SingleOrDefault(x =>
                    x.Type == "CandidateId");
                if (currentClaim != null)
                {
                    await _studentUserManager.ReplaceClaimAsync(student, currentClaim, new Claim("CandidateId",
                        candidate.CandidateId.ToString()));
                }
                else
                {
                    await _studentUserManager.AddClaimAsync(student,
                        new Claim("CandidateId", candidate.CandidateId.ToString()));
                }

                return RedirectToAction("Index", new {id = sessionId});
            }

            return View();
        }

       
        
        [Authorize(Roles = ValidRoles.Deputy + "," + ValidRoles.HeadMaster)]
        public async Task<IActionResult> RemoveCandidate(int id)
        {
            var candidate = await _candidateRepository.FindCandidateById(id);
            return View(candidate);
        }

        public async Task<IActionResult> RemoveConfirm(int id)
        {
            var candidate = await _candidateRepository.FindCandidateById(id);
            if (candidate != null)
            {
                _candidateRepository.Remove(candidate);
                await _candidateRepository.SaveChangesAsync();
            }

            return RedirectToAction("Index", "VoteSession");
        }

        [HttpGet]
        public async Task<IActionResult> SelectStudent(int id)
        {
            var nonCandidateStudents = await _studentRepository.NotCandidateStudents(id);
            return View(nonCandidateStudents);
        }
    }
}
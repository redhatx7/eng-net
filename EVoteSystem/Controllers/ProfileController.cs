using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EVoteSystem.Models;
using EVoteSystem.Repositories;
using EVoteSystem.Services;
using EVoteSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EVoteSystem.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET
        private readonly ICandidateRepository _candidateRepository;

        private readonly IProfileRepository _profileRepository;
        
        public ProfileController(ICandidateRepository candidateRepository,
            IProfileRepository profileRepository)
        {
            _candidateRepository = candidateRepository;
            _profileRepository = profileRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Me(int id)
        {
            var candidate = await _candidateRepository.FindCandidateById(id);
            if (candidate == null)
                return RedirectToAction("Index", "Home");
            var profiles = await _profileRepository.GetCandidateProfileByIdAsync(id);
            var student = candidate.Student;
            return View(new Tuple<Student, IList<Profile>>(student, profiles));
        }

       
       

        [HttpGet]
        public async Task<IActionResult> AddProfile()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = ValidRoles.Candidate)]
        public async Task<IActionResult> AddProfile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var candidateIdClaim = ((ClaimsIdentity) User.Identity)?.FindFirst("CandidateID")?.Value;
                if (string.IsNullOrEmpty(candidateIdClaim))
                    throw new Exception("Candidate ID is null");
                int candidateId = int.Parse(candidateIdClaim);
                var candidate = await _candidateRepository.FindCandidateById(candidateId);
                if (candidate == null || !candidate.IsValidCandidate)
                {
                    return RedirectToAction("MainPage", "Home");
                }

                long fileSize = model.MediaFile?.Length ?? 0;
                string filePath = "";
                if (fileSize > 0)
                {
                    string filename = model.MediaFile?.FileName ?? "";
                    string ext = filename.Split('.').Last().ToLower();
                    if (ext.Contains("mp4") || ext.Contains("jpg") || ext.Contains("png") || ext.Contains("jpeg"))
                    {
                        filePath = $"/wwwroot/medias/{Guid.NewGuid()}.{ext}";
                        Directory.CreateDirectory(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.MediaFile?.CopyToAsync(stream);
                            await stream.FlushAsync();

                        }
                    }
                    

                }

                var profile = new Profile()
                {
                    Candidate = candidate,
                    Description = model.Description,
                    MediaContentPath = filePath
                };
                await _profileRepository.Insert(profile);
                await _profileRepository.SaveChangesAsync();
                return RedirectToAction("Me");
            }
            ModelState.AddModelError(null, "Check the inputs");
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ValidRoles.Candidate)]
        public async Task<IActionResult> RemoveProfile(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            string candidateIdClaim = ((ClaimsIdentity) User.Identity)?.FindFirst("CandidateId")?.Value ?? "0";
            int candidateId = int.Parse(candidateIdClaim);
            var profile = await _profileRepository.FindProfileByIdAsync(id);
            if (profile != null && profile.Candidate.CandidateId == candidateId)
            {
                return View(profile);
            }

            return RedirectToAction("Index", "Home");
        }
        
        [Authorize(Roles = ValidRoles.Candidate)]
        [HttpPost, ActionName("DeleteProfile")]
        public async Task<IActionResult> DeleteProfileConfirm(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string candidateIdClaim = ((ClaimsIdentity) User.Identity)?.FindFirst("CandidateId")?.Value;
            int candidateId = int.Parse(candidateIdClaim ?? "0");
            var profile = await  _profileRepository.FindProfileByIdAsync(id);
            if (profile != null && profile.Candidate.CandidateId == candidateId)
            {
                _profileRepository.Remove(profile);
                await _profileRepository.SaveChangesAsync();
            }

            return RedirectToAction("Me", new {id = candidateId});
        }

        
    }
}
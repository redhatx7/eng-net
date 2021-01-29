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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace EVoteSystem.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private ILogger<StudentController> _logger;

        private IStudentRepository _studentRepository;

        private UserManager<Student> _userManager;

        public StudentController(ILogger<StudentController> logger, IStudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentRepository.GetAll();
            return View(students);
        }


        [HttpGet]
        [Authorize(Roles = ValidRoles.HeadMaster + "," + ValidRoles.Deputy)]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ValidRoles.HeadMaster + "," + ValidRoles.Deputy)]
        public async Task<IActionResult> AddStudent(StudentRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isRegistredBefore = (await _studentRepository.FindStudentByNationalId(model.NationalCode)) == null;
                if (isRegistredBefore)
                {
                    ModelState.AddModelError(null,"This student is registered before");
                    return View();
                }
                
                long fileSize = model.PersonalImage?.Length ?? 0;
                string filePath = "";
                if (fileSize > 0)
                {
                    string filename = model.PersonalImage?.FileName ?? "";
                    string ext = filename.Split('.').Last().ToLower();
                    if (ext.Contains("jpg") || ext.Contains("png") || ext.Contains("jpeg"))
                    {
                        filePath = $"/wwwroot/photos/{Guid.NewGuid()}.{ext}";
                        Directory.CreateDirectory(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.PersonalImage?.CopyToAsync(stream);
                            await stream.FlushAsync();
                        }
                    }
                }
                var student = new Student()
                {
                    Birthday = model.Birthday,
                    Email = model.Email,
                    Name = model.Surename,
                    Surename = model.Surename,
                    NationalCode = model.NationalCode,
                    UserName = model.Username,
                    Grade = model.Grade,
                    PersonalImagePath = filePath
                };
                var result = await _userManager.CreateAsync(student, model.Password);
                if (result.Succeeded)
                {
                    var claim = new Claim("Fullname", student.Fullname);
                    await _userManager.AddClaimAsync(student, claim);
                    await _userManager.AddToRoleAsync(student, ValidRoles.Student);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(null, "Please Check The inputs");
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ValidRoles.HeadMaster + "," + ValidRoles.Deputy)]
        public async Task<IActionResult> EditStudent(int? id)
        {
            var student = await  _studentRepository.FindStudentByIdAsync(id);
            if (student == null)
            {
                return RedirectToAction("Index");
            }

            var studentView = new StudentRegisterViewModel()
            {
                Birthday = student.Birthday,
                Email = student.Email,
                Grade = student.Grade,
                Name = student.Name,
                Password = "",
                Surename = student.Surename,
                Username = student.UserName,
                NationalCode = student.NationalCode
            };
            return View(studentView);
        }

        [HttpPost]
        [Authorize(Roles = ValidRoles.HeadMaster + "," + ValidRoles.Deputy)]
        public async Task<IActionResult> EditStudent(Student model)
        {
            if (ModelState.IsValid)
            {
                var student = await _studentRepository.FindStudentByIdAsync(model.Id);
                if (student == null) return RedirectToAction("Index");
                student.Name = model.Name;
                student.Surename = model.Surename;
                student.Birthday = model.Birthday;
                student.Email = model.Email;
                student.NationalCode = model.NationalCode;
                _studentRepository.Update(student);
                await _studentRepository.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(null, "Please Check the inputs");
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ValidRoles.HeadMaster + "," + ValidRoles.Deputy)]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            var student = await _studentRepository.FindStudentByIdAsync(id);
            if (student == null)
                return RedirectToAction("Index");
            student.PasswordHash = null;
            return View(student);
        }

        [HttpPost, ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ValidRoles.HeadMaster + "," + ValidRoles.Deputy)]
        public async Task<IActionResult> DeleteStudentConfirm(int? id)
        {
            var student = await _userManager.FindByIdAsync(id.ToString());
            if (student == null)
                return RedirectToAction("Index");
            await _userManager.DeleteAsync(student);
            return RedirectToAction("Index");
        }



    }
}
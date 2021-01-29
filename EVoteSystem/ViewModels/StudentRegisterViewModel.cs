using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EVoteSystem.Models;
using Microsoft.AspNetCore.Http;

namespace EVoteSystem.ViewModels
{
    public class StudentRegisterViewModel
    {
        
        [Required(ErrorMessage = "نام را وارد کنید")]
        [MinLength(3, ErrorMessage = "حداقل طول نام باید ۳ باشد.")]
        [Display(Name = "نام")]
        public string Name { get; set; }
        
        
        
        [Required(ErrorMessage = "نام‌خانوادگی را وارد کنید.")]
        [MinLength(4, ErrorMessage = "حداقل طول نام‌خانوادگی باید ۴ باشد.")]
        [Display(Name = "نام خانوادگی")]
        public string Surename { get; set; }
        
        [NotMapped]
        public string Fullname => $"{Name} {Surename}";
        
        [Required(ErrorMessage = "کد ملی را وارد کنید.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی باید ۱۰ رقمی باشد")]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }
        
        
        [Display(Name =  "تاریخ تولد")]
        public DateTime Birthday { get; set; }
        
        [Display(Name = "پایه")]
        public Grade Grade { get; set; }

        public IFormFile PersonalImage { get; set; }

        [Required]
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }
        
        [Required]
        [MinLength(4)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "پست الکترونیک")]
        public string Email { get; set; }
    }
}
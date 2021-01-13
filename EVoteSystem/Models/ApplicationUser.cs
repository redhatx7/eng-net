using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EVoteSystem.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required(ErrorMessage = "نام را وارد کنید")]
        [MinLength(3, ErrorMessage = "حداقل طول نام باید ۳ باشد.")]
        [Display(Name = "نام")]
        public string Name { get; set; }
        
        
        
        [Required(ErrorMessage = "نام‌خانوادگی را وارد کنید.")]
        [MinLength(4, ErrorMessage = "حداقل طول نام‌خانوادگی باید ۴ باشد.")]
        [Display(Name = "نام‌خانوادگی")]
        public string Surename { get; set; }
        
        [NotMapped]
        public string Fullname => $"{Name} {Surename}";
        
        [Required(ErrorMessage = "کد ملی را وارد کنید.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی باید ۱۰ رقمی باشد")]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }
        
        
        [Display(Name =  "تاریخ تولد")]
        public DateTime Birthday { get; set; }
   
        
    }
}
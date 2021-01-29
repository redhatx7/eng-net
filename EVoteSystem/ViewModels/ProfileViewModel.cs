using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EVoteSystem.ViewModels
{
    public class ProfileViewModel
    {
        [Display(Name = "متن")]
        [Required(ErrorMessage = "متن نمی‌تواند خالی باشد.")]
        public string Description { get; set; }
        

        [Display(Name = "عکس یا فیلم")]
        public IFormFile MediaFile { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedAt { get; set; }

        public ProfileViewModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Models
{
    public class Profile
    {
        [Key]
        [Display(Name = "شناسه پروفایل")]
        public int ProfileId { get; set; }
        
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "وارد کردن توضیحات الزامی است.")]
        public string Description { get; set; }

        [Display(Name =  "تصویر پروفایل")]
        public string ProfileImage { get; set; }
        
        
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
    }
}
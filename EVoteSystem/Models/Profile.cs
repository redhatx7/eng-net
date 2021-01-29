using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Models
{
    public class Profile
    {
        [Key]
        [Display(Name = "شناسه")]
        public int ProfileId { get; set; }
        
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "وارد کردن توضیحات الزامی است.")]
        public string Description { get; set; }

        [Display(Name =  "تصویر  یا فیلم")]
        public string MediaContentPath { get; set; }

        public string FileExtension => MediaContentPath.Split('.').Last();
        
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        
        public Candidate Candidate { get; set; }
    }
}
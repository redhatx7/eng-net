using System.ComponentModel.DataAnnotations;

namespace EVoteSystem.ViewModels
{
    public enum LoginType : short
    {
        [Display(Name = "کاندید")]
        Candidate,
        [Display(Name = "دانش‌آموز")]
        Student,
        [Display(Name = "مدیر")]
        HeadMaster,
        [Display(Name = "معاون")]
        Deputy
    }
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "نام کاربری را وارد کنید.")]
        public string Username { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "کلمه‌عبور را وارد کنید")]
        public string Password { get; set; }
        
        [Display(Name = "به یاد داشتن؟")]
        public bool RememberMe { get; set; }
        
        [Display(Name = "ورود به عنوان")]
        public LoginType LoginType { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace EVoteSystem.Models
{
    public class ApplicationAdmin : ApplicationUser
    {
        [Display(Name =  "تصویر نمایه")]
        public string Image { get; set; }

        public ApplicationAdmin()
        {
            Image = "/wwwroot/images/default-profile.jpg";
        }

        public ApplicationAdmin(string imageUrl)
        {
            Image = imageUrl;
        }
    }
}
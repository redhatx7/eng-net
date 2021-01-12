using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EVoteSystem.Models
{
    public enum Grade
    {
        [Display(Name = "هفتم")]
        Seventh = 7,
        [Display(Name = "هشتم")]
        Eighth = 8,
        [Display(Name = "نهم")]
        Ninth = 9,
        [Display(Name = "دهم")]
        Tenth = 10,
        [Display(Name = "یازدهم")]
        Eleventh = 11,
        [Display(Name = "دوازدهم")]
        Twelfth = 12
    }
    
    public class Student : ApplicationUser
    {
        [Display(Name = "پایه")]
        public Grade Grade { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace EVoteSystem.Models
{
    public class VoteSession
    {   
        [Key]
        public int VoteSessionId { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "سال تحصیلی")]
        public string Year { get; set; }

        public ICollection<Candidate> Candidates { get; set; }

        [Display(Name = "فعال")] 
        public bool IsActive { get; set; }

        public VoteSession(string description, string year)
        {
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using EVoteSystem.Services;

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

        public string Year => CreatedAt.GetCurrentYear();
        
        [Display(Name = "فعال")] 
        public bool IsActive { get; set; }

        public string Status => IsActive ? "فعال" : "بسته شده";

        public ICollection<Candidate> Candidates { get; set; }
        
        public ICollection<Vote> SessionVotes { get; set; }
        
        public VoteSession(string description, string year)
        {
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        public VoteSession()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
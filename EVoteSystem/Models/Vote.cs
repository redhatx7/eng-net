using System;
using System.ComponentModel.DataAnnotations;

namespace EVoteSystem.Models
{
    public class Vote
    {
        [Key]
        [Display(Name = "شناسه رای")]
        public int VoteId { get; set; }
        
        [Display(Name = "تاریخ رای")]
        public DateTime CreatedAt { get; set; }
        public Student FromStudent { get; set; }
        public Candidate ToCandidate { get; set; }
        
        public VoteSession Session { get; set; }

        public Vote()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
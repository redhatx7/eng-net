using System;
using System.ComponentModel.DataAnnotations;

namespace EVoteSystem.Models
{
    public class Vote
    {
        [Key]
        [Display(Name = "شناسه رای")]
        public int VoteId { get; set; }

        public Student FromStudent { get; set; }

        public Candidate ToCandidate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
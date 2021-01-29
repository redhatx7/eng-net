using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVoteSystem.Models
{
    public class Candidate 
    {
        [Display(Name = "شناسه کاندید")]
        [Key]
        public int CandidateId { get; set; }
        
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Display(Name = "شعار")]
        [MinLength(4)]
        public string Slogan { get; set; }
        public Student Student { get; set; }
        public VoteSession Session { get; set; }
        
        public ICollection<Vote> Votes { get; set; }

        public ICollection<Profile> Profiles { get; set; }
        public bool IsValidCandidate => Session?.IsActive == true;
    }
}
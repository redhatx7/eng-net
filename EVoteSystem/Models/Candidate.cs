using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EVoteSystem.Models
{
    public class Candidate 
    {
        [Display(Name = "شناسه کاندید")]
        [Key]
        public int CandidateId { get; set; }
        
        public Student Student { get; set; }

        public VoteSession Session { get; set; }
        
        public ICollection<Vote> Votes { get; set; }

        public ICollection<Profile> Profiles { get; set; }
    }
}
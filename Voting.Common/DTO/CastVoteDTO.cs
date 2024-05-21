using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.Common.DTO
{
    public class CastVoteDTO
    {
        [Required]
        public string Voter { get; set; }
        [Required]
        public string Candidate { get; set; }
    }
}

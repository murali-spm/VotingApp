using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Voting.Common.DTO
{
    public class ElectionDTO
    {
        public int ID { get; set; }
        public List<VoterDTO>? Voters { get; set; }
        public List<CandidateDTO>? Candidates { get; set; }

        public CastVoteDTO? CastVoteDTO { get; set; }
    }
}

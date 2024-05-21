using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Common.DTO;
using Voting.Common;

namespace Voting.DataAccess
{
    public abstract class DataAccessBase
    {
        private List<CastVoteDTO> GetVotes()
        {
            List<CastVoteDTO> totalVotes = new List<CastVoteDTO>();
            List<string> votes = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + Settings.VOTES_FILE).ToList();
            foreach (string vote in votes)
            {
                string[] data = vote.Split("||");
                CastVoteDTO castVote = new CastVoteDTO()
                {
                    Voter = data[0],
                    Candidate = data[1]
                };
                totalVotes.Add(castVote);
            }
            return totalVotes;
        }

        internal void GetVoteCount(List<CandidateDTO> candidateDTOs)
        {
            List<CastVoteDTO> totalVotes = GetVotes();

            foreach (CandidateDTO candidate in candidateDTOs)
            {
                int total = totalVotes.Count(x => x.Candidate == candidate.Name);
                candidate.TotalVotes = total;
            }
        }

        internal void GetVoteCastDetails(List<VoterDTO> voterDTOs)
        {
            List<CastVoteDTO> votes = GetVotes();

            foreach (VoterDTO voter in voterDTOs)
            {
                if (votes.FirstOrDefault(x => x.Voter == voter.Name) != null)
                {
                    voter.HasVoted = true;
                }
            }
        }
    }
}

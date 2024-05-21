using Voting.Common;
using Voting.Common.DTO;
using Voting.DataAccess;

namespace Voting.Service
{
    public class VoterService
    {
        public List<VoterDTO>? GetVotersList()
        {
            VoterDataAccess voterDB = new VoterDataAccess();
            return voterDB.GetVotersList();
        }

        public void AddVoter(VoterDTO voterDto, ref bool isVoterExists)
        {
            if(voterDto != null && !string.IsNullOrEmpty(voterDto.Name))
            {
                VoterDataAccess voterDB = new VoterDataAccess();
                voterDB.AddVoter(voterDto, ref isVoterExists);
            }
        }

        public void CastVote(CastVoteDTO castVoteDTO)
        {
            if (castVoteDTO != null && !string.IsNullOrEmpty(castVoteDTO.Voter) && !string.IsNullOrEmpty(castVoteDTO.Candidate))
            {
                VoterDataAccess voterDB = new VoterDataAccess();
                voterDB.CastVote(castVoteDTO);
            }
        }

        public bool IsAlreadyVoted(CastVoteDTO castVoteDTO)
        {
            bool isVoted = false;
            if (castVoteDTO != null)
            {
                VoterDataAccess voterDB = new VoterDataAccess();
                isVoted = voterDB.IsAlreadyVoted(castVoteDTO.Voter);
            }
            return isVoted;
        }

        public void ResetAll()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + Settings.VOTERS_LIST_FILE;
            if (System.IO.File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
            }

            path = AppDomain.CurrentDomain.BaseDirectory + Settings.CANDIDATES_LIST_FILE;
            if (System.IO.File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
            }

            path = AppDomain.CurrentDomain.BaseDirectory + Settings.VOTES_FILE;
            if (System.IO.File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
            }

           
        }
    }
}

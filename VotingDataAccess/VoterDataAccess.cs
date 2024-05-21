using System.Data.Common;
using System.Runtime.InteropServices.Marshalling;
using Voting.Common;
using Voting.Common.DTO;

namespace Voting.DataAccess
{
    public class VoterDataAccess : DataAccessBase
    {
        public List<VoterDTO>? GetVotersList()
        {
            //Get the voters from database using storedprocedure.
            //In this sample, get the voters list from the file voters.txt
            List<VoterDTO>? voters = null ;
            
            string path = AppDomain.CurrentDomain.BaseDirectory + Settings.VOTERS_LIST_FILE;
            if (System.IO.File.Exists(path))
            {
                voters = new List<VoterDTO>();
                List<string> lines =  File.ReadLines(path).ToList();
                foreach(string line in lines)
                {
                    string[] data = line.Split("||");
                    voters.Add(new VoterDTO { Name = data[0], HasVoted = bool.Parse(data[1]) });
                }
            }

            if (voters != null)
            {
                GetVoteCastDetails(voters);
            }

            return  voters;
        }

        public void AddVoter(VoterDTO voterDto,ref bool isVoterExists)
        {
            List<VoterDTO>? voters = GetVotersList();

            if(voters != null)
            {
                VoterDTO? voter = voters.FirstOrDefault(x => x.Name.ToLower().Trim() == voterDto.Name.ToLower().Trim());

                if (voter != null)
                {
                    isVoterExists = true;
                }
            }

            if (!isVoterExists)
            {
                //add the voter into database using storedprocedure.
                //In this sample, adding the voter to the list in the file voters.txt
                string path = AppDomain.CurrentDomain.BaseDirectory + Settings.VOTERS_LIST_FILE;
                if (System.IO.File.Exists(path))
                {
                    string[] data = new string[] { voterDto.Name.Trim() + "||" + voterDto.HasVoted.ToString() };
                    File.AppendAllLines(path, data);
                }
            }
        }

        public void CastVote(CastVoteDTO castVoteDTO)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + Settings.VOTES_FILE;
            string[] data = new string[] { castVoteDTO.Voter.Trim() + "||" + castVoteDTO.Candidate.Trim() };
            File.AppendAllLines(path,data);
        }

        private void GetVoteDetails(List<VoterDTO> voterDTOs)
        {
            GetVoteCastDetails(voterDTOs);
        }

        public bool IsAlreadyVoted(string voter)
        {
            bool isVoted = false;
            List<VoterDTO>? voters =  GetVotersList();
            if (voters != null)
            {
                VoterDTO? voterDTO = voters.FirstOrDefault(x => x.Name == voter && x.HasVoted == true);

                if(voterDTO!=null)
                {
                    isVoted =  true;
                }    
            }
            return isVoted;
        }
    }
}

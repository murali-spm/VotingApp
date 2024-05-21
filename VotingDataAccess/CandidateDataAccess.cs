using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Common.DTO;
using Voting.Common;

namespace Voting.DataAccess
{
    public class CandidateDataAccess : DataAccessBase
    {
        public List<CandidateDTO>? GetCandidatesList()
        {
            //Get the voters from database using storedprocedure.
            //In this sample, get the voters list from the file candidates.txt
            List<CandidateDTO>? candidates = null;

            string path = AppDomain.CurrentDomain.BaseDirectory + Settings.CANDIDATES_LIST_FILE;
            if (System.IO.File.Exists(path))
            {
                candidates = new List<CandidateDTO>();
                List<string> lines = File.ReadLines(path).ToList();
                foreach (string line in lines)
                {
                    string[] data = line.Split("||");
                    candidates.Add(new CandidateDTO { Name = data[0], TotalVotes = int.Parse(data[1]) });
                }
            }

            if(candidates!= null)
            {
                GetVoteCount(candidates);
            }

            return candidates;
        }

        public void AddCandidate(CandidateDTO candidateDto, ref bool isCandidteExits)
        {
            List<CandidateDTO>? candidates = GetCandidatesList();

            if (candidates != null)
            {
                CandidateDTO? candidate = candidates.FirstOrDefault(x => x.Name.ToLower().Trim() == candidateDto.Name.ToLower().Trim());

                if (candidate != null)
                {
                    isCandidteExits = true;
                }
            }

            if (!isCandidteExits)
            {
                //add the voter into database using storedprocedure.
                //In this sample, adding the voter to the list in the file candidates.txt
                string path = AppDomain.CurrentDomain.BaseDirectory + Settings.CANDIDATES_LIST_FILE;
                if (System.IO.File.Exists(path))
                {
                    string[] data = new string[] { candidateDto.Name.Trim() + "||" + candidateDto.TotalVotes.ToString() };
                    File.AppendAllLines(path, data);
                }
            }
        }
    }
}

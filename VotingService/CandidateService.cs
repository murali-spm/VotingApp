using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Common.DTO;
using Voting.DataAccess;

namespace Voting.Service
{
    public class CandidateService
    {
        public List<CandidateDTO>? GetCandidatesList()
        {
            CandidateDataAccess candidateDB = new CandidateDataAccess();
            return candidateDB.GetCandidatesList();
        }

        public void AddCandidate(CandidateDTO candidateDto, ref bool isCandidateExists)
        {
            if (candidateDto != null && !string.IsNullOrEmpty(candidateDto.Name))
            {
                CandidateDataAccess candidateDB = new CandidateDataAccess();
                candidateDB.AddCandidate(candidateDto, ref isCandidateExists);
            }
        }
    }
}

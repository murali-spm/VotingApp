using Microsoft.AspNetCore.Mvc;
using Voting.Common.DTO;
using Voting.Service;

namespace VotingApp.Controllers
{
    public class VoterController : Controller
    {
        VoterService _voterService = new VoterService();
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddVoter(VoterDTO voterDto)
        {
            bool isVoterExits = false;
            if (!ModelState.IsValid)
            {
                return View(voterDto);
            }
            _voterService.AddVoter(voterDto, ref isVoterExits);

            if(isVoterExits)
            {
                TempData["Error"] = "Voter already exists.";
            }
            else
            {
                TempData["Success"] = "Voter added successfully.";
            }

            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public IActionResult CastVote(ElectionDTO electionDTO)
        {   

            if(electionDTO == null || electionDTO.CastVoteDTO == null || electionDTO.CastVoteDTO.Candidate == null ||
                    string.IsNullOrEmpty(electionDTO.CastVoteDTO.Voter) || string.IsNullOrEmpty(electionDTO.CastVoteDTO.Candidate))
            {
                TempData["Error"] = "Voter and Candidate must be selected.";
                return RedirectToAction("index", "home");
            }

            if (_voterService.IsAlreadyVoted(electionDTO.CastVoteDTO))
            {
                TempData["Error"] = "This voter has already voted.";
                return RedirectToAction("index", "home");
            }
            TempData["Success"] = "Vote casted successfully";

            _voterService.CastVote(electionDTO.CastVoteDTO);
            return RedirectToAction("index", "home");
        }
    }
}

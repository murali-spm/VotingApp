using Microsoft.AspNetCore.Mvc;
using Voting.Common.DTO;
using Voting.Service;

namespace VotingApp.Controllers
{
    public class CandidateController : Controller
    {

        CandidateService _candidateService = new CandidateService();
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCandidate(CandidateDTO candidateDTO)
        {
            bool isCandidateExists = false;
            if (!ModelState.IsValid)
            {
                return View(candidateDTO);
            }
            _candidateService.AddCandidate(candidateDTO, ref isCandidateExists);

            if (isCandidateExists)
            {
                TempData["Error"] = "This candidate already exists.";
            }
            else
            {
                TempData["Success"] = "Candidate added successfully.";
            }

            return RedirectToAction("index", "home");
        }
    }
}

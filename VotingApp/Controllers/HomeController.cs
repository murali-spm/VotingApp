using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Voting.Common.DTO;
using Voting.Service;
using VotingApp.Models;

namespace VotingApp.Controllers
{   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        VoterService _voterService = new VoterService();
        CandidateService _candidateService = new CandidateService();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {   
            List<VoterDTO>? voters = _voterService.GetVotersList();
            List<CandidateDTO>? candidates = _candidateService.GetCandidatesList();

            ElectionDTO electionDTO = new ElectionDTO() {
                Voters = voters,
                Candidates = candidates,
                CastVoteDTO = new CastVoteDTO()
            };
            return View( electionDTO);
        }

        public IActionResult Reset()
        {
            _voterService.ResetAll();
            return RedirectToAction("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

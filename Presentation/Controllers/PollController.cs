using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Presentation.ActionFilters;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        public IActionResult Index([FromServices] IPollRepository _pollRepo)
        {
            var polls = _pollRepo.GetPolls();
            var orderedPolls = polls.OrderByDescending(p => p.CreatedAt);
            return View(orderedPolls);
        }

        [HttpGet]
        public IActionResult Create([FromServices] IPollRepository _pollRepo)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Poll p, [FromServices] IPollRepository _pollRepo, [FromServices] IWebHostEnvironment host)
        {
            if (ModelState.IsValid)
            {
                p.Option1VotesCount = 0;
                p.Option2VotesCount = 0;
                p.Option3VotesCount = 0;
                p.CreatedAt = DateTime.Now;
                _pollRepo.AddPoll(p);
                TempData["message"] = "Student was added successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        [VotingActionFilter()]
        public IActionResult Vote(int id, [FromServices] IPollRepository _pollRepo)
        {
            Poll poll = _pollRepo.GetPoll(id);
            if (poll == null)
            {
                return NotFound();
            }

            VoteModel model = new VoteModel
            {
                Id = poll.Id,
                choice = 0
            };

            ViewBag.PollId = id;
            ViewBag.Title = poll.Title;
            ViewBag.Option1Text = poll.Option1Text;
            ViewBag.Option2Text = poll.Option2Text;
            ViewBag.Option3Text = poll.Option3Text;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddVote(VoteModel model, [FromServices] IPollRepository _pollRepo, [FromServices] PollUserRepository _voteRepo)
        {
            if (ModelState.IsValid)
            {
                _pollRepo.Vote(model.Id, model.choice);
                PollUser pollUser = new PollUser
                {
                    PollId = model.Id,
                    UserId = User.Identity.Name,
                };
                _voteRepo.AddVote(pollUser);
                TempData["message"] = "Voting was successfull";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Results(int id, [FromServices] IPollRepository _pollRepo) 
        {
            Poll poll = _pollRepo.GetPoll(id);
            if (poll == null)
            {
                return NotFound();
            }

            return View(poll);
        }

    }
}

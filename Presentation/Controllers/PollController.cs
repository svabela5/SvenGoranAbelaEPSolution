using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        public IActionResult Index([FromServices] PollRepository _pollRepo)
        {
            var polls = _pollRepo.GetPolls();
            var orderedPolls = polls.OrderByDescending(p => p.CreatedAt);
            return View(orderedPolls);
        }

        [HttpGet]
        public IActionResult Create([FromServices] PollRepository _pollRepo)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Poll p, [FromServices] PollRepository _pollRepo, [FromServices] IWebHostEnvironment host)
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
    }
}

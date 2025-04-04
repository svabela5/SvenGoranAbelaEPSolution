using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        public IActionResult Index([FromServices] PollRepository _pollRepo)
        {
            var polls = _pollRepo.GetPolls();
            return View(polls);
        }
    }
}

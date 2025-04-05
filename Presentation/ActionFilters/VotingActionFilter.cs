using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.ActionFilters
{
    public class VotingActionFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User != null)
            {
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    var pollId = context.ActionArguments["id"] as int?;

                    if (pollId != null)
                    {
                        var user = context.HttpContext.User.Identity.Name;
                        var _voteRepo = context.HttpContext.RequestServices.GetService(typeof(PollUserRepository)) as PollUserRepository;
                        if (hasUserVoted(user, pollId.Value, _voteRepo))
                        {
                            context.Result = new RedirectToActionResult("Index", "Poll", new { pollId });
                            return;

                        }
                    }
                }
            }
            base.OnActionExecuting(context);
        }

        Boolean hasUserVoted(string user, int pollId, [FromServices] PollUserRepository _voteRepo)
        {
            return _voteRepo.GetVote(user, pollId) != null;
        }
    }
}

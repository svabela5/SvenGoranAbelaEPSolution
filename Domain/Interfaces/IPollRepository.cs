using Domain.Models;

namespace DataAccess.Repositories
{
    public interface IPollRepository
    {
        void AddPoll(Poll poll);
        Poll GetPoll(int id);
        IQueryable<Poll> GetPolls();
        void Vote(int id, int choice);
    }
}
using DataAccess.DataContext;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PollRepository
    {
        private PollDbContext myContext;
        public PollRepository(PollDbContext _myContext) 
        {
            myContext = _myContext;
        }

        public IQueryable<Poll> GetPolls()
        {
            return myContext.Polls;
            
        }

        public Poll GetPoll(int id)
        {
            return myContext.Polls.FirstOrDefault(p => p.Id == id);
        }

        public void AddPoll(Poll poll)
        {
            myContext.Polls.Add(poll);
            myContext.SaveChanges();
        }

        public void Vote(int id, int choice)
        {
            var poll = GetPoll(id);

            if (poll != null)
            {
                switch (choice)
                {
                    case 1:
                        poll.Option1VotesCount++;
                        break;
                    case 2:
                        poll.Option2VotesCount++;
                        break;
                    case 3:
                        poll.Option3VotesCount++;
                        break;
                    default:
                        throw new ArgumentException("Invalid choice");
                }
                myContext.SaveChanges();
            }
        }
    }
}

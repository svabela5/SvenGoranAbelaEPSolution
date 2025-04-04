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
            PollDbContext myContext = _myContext;
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

        public void UpdatePoll(Poll poll)
        { 
        }

        public void DeletePoll(int id)
        { }
    }
}

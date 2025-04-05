using DataAccess.DataContext;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PollUserRepository
    {
        private PollDbContext myContext;
        public PollUserRepository(PollDbContext _myContext)
        {
            myContext = _myContext;
        }

        public IQueryable<PollUser> GetVotes()
        {
            return myContext.PollUsers;

        }

        public PollUser GetVote(string userId,int pollId)
        {
            return GetVotes().FirstOrDefault(p => p.UserId.Equals(userId) && p.PollId == pollId);
        }

        public void AddVote(PollUser pollUser)
        {
            pollUser.CreatedAt = DateTime.Now;
            myContext.PollUsers.Add(pollUser);
            myContext.SaveChanges();
        }
    }
}

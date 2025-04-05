using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PollFileRepository: IPollRepository
    {
        string filePath;

        public PollFileRepository(IConfiguration configuration)
        {
            // Retrieve the filename from appsettings.json
            filePath = configuration["PollsFileName"];
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("Log file path is not configured in appsettings.json.");
            }
        }

        public IQueryable<Poll> GetPolls()
        {
            if (!File.Exists(filePath))
            {
                return new List<Poll>().AsQueryable();
            }
            string json = File.ReadAllText(filePath);

            if (json == null)
            {
                return new List<Poll>().AsQueryable();
            }

            return JsonConvert.DeserializeObject<List<Poll>>(json).AsQueryable<Poll>();
        }
        public Poll GetPoll(int id)
        {
            return GetPolls().FirstOrDefault(p => p.Id == id);
        }
        public void AddPoll(Poll poll)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]"); // Creates an empty JSON array
            }

            var polls = GetPolls().ToList();

            if (polls == null)
            {
                polls = new List<Poll>();
            }

            poll.Id = polls.Count > 0
                ? polls.Max(u => u.Id) + 1
                : 1; // if the list is empty, start from 1


            polls.Add(poll);
            string json = JsonConvert.SerializeObject(polls, Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Poll added successfully.");
        }
        public void Vote(int id, int choice)
        {
            throw new NotImplementedException();
        }
    }
}

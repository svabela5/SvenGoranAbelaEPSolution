using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PollUser
    {
        [Key]
        public int Id { get; set; }
        public int PollId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        // Navigation properties
        [ForeignKey("PollId")]
        public virtual Poll Poll { get; set; }
    }
}

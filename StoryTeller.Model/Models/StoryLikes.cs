using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTeller.Domain.Models
{
    public class StoryLikes
    {
        public int ID { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Story Story { get; set; }
    }
}

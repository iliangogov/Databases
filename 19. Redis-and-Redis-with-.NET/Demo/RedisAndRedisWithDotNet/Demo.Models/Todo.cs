using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Todo
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public User AssignedTo { get; set; }

        public DateTime Deadline { get; set; }

        public bool IsDone { get; set; }
    }
}

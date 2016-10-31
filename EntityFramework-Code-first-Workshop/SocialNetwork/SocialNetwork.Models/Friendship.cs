using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class Friendship
    {
        private ICollection<Message> messages;

        public Friendship()
        {
            this.messages = new HashSet<Message>();
        }

        public int Id { get; set; }

        public int FirstUserId { get; set; }

        public User FirstUser { get; set; }

        public int SecondUserId { get; set; }

        public User SecondUser { get; set; }

        [Index]
        public bool Approved { get; set; }

        public DateTime FriendshipSince { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}

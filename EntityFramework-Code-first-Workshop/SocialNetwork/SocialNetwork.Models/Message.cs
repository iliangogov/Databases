using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class Message
    {
        // Asotiated with friendship
        public Message()
        {

        }

        public int Id { get; set; }

        public int FriendshipId { get; set; }

        public virtual Friendship Friendship { get; set; }

        public int AuthorId { get; set; }

        public virtual User Author { get; set; }

        [Required]
        public string Content { get; set; }

        [Index]
        public DateTime SentOn { get; set; }

        public DateTime? SeenOn { get; set; }
    }
}

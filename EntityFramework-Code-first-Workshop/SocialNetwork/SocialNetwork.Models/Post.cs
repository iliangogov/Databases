using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class Post
    {
        private ICollection<User> taggedUsers;

        public Post()
        {
            this.taggedUsers = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(10,ErrorMessage ="Invalid Post Content Length")]
        public string Content { get; set; }

        public DateTime PostedOn { get; set; }
       
        public virtual ICollection<User> TaggedUsers
        {
            get { return this.taggedUsers; }
            set { this.taggedUsers = value; }
        }
    }
}

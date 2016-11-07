using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    public class Post
    {
        private ICollection<UserProfile> taggedUsers;

        public Post()
        {
            this.taggedUsers = new HashSet<UserProfile>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Invalid Post Content Length")]
        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public virtual ICollection<UserProfile> TaggedUsers
        {
            get { return this.taggedUsers; }
            set { this.taggedUsers = value; }
        }
    }
}

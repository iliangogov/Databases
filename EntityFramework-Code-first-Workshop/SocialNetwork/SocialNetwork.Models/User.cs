using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class User
    {
        private ICollection<Message> messages;
        private ICollection<Image> images;
        private ICollection<Post> posts;
        private DateTime registeredOn;

        public User()
        {
            this.messages = new HashSet<Message>();
            this.images = new HashSet<Image>();
            this.posts = new HashSet<Post>();
        }

        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MinLength(4)]
        [MaxLength(20)]
        // [Range(4, 20, ErrorMessage = "Invalid User Username length")]
        public string Username { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        // [Range(2, 50, ErrorMessage = "Invalid User FirstName length")]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        // [Range(2, 50,ErrorMessage ="Invalid User LastName length")]
        public string LastName { get; set; }

        public DateTime RegisteredOn
        {
            get { return this.registeredOn; }
            set { this.registeredOn = value; }
        }

        public virtual ICollection<Message> Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
        }

        public virtual ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public virtual ICollection<Post> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }
    }
}

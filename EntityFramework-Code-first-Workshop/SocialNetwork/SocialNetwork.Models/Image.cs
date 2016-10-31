using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public User User { get; set; }

        [MaxLength(4,ErrorMessage ="Invalid FileExtantion length")]
        public string FileExtantion { get; set; }
    }
}

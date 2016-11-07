using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public UserProfile User { get; set; }

        [MaxLength(4, ErrorMessage = "Invalid FileExtantion length")]
        public string FileExtension { get; set; }
    }
}

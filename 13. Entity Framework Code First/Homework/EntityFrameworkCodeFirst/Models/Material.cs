using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Material
    {
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [Required]
        public string Content { get; set; }
    }
}

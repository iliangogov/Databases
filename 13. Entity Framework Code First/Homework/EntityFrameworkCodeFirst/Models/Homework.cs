using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Homework
    {
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [Required]
        public string Content { get; set; }

        public DateTime TimeSent { get; set; }

    }
}

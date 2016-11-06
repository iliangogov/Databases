namespace Cars.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Car
    {
        public int Id { get; set; }

        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [Required]
        [MaxLength(30)] // 11 not working with provided json
        [Index(IsUnique = true)]
        public string Model { get; set; }

        public TransmissionType TransmissionType { get; set; }

        public short Year { get; set; }

        public decimal Price { get; set; }

        public int DealerId { get; set; }

        public virtual Dealer Dealer { get; set; }
    }
}

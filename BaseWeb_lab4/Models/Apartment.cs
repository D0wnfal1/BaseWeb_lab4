using System.ComponentModel.DataAnnotations;

namespace BaseWeb_lab4.Models
{
    public class Apartment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string District { get; set; } = string.Empty;

        [Required]
        [Range(1, 50)]
        public int Floor { get; set; }

        [Required]
        [StringLength(100)]
		public string Area { get; set; }

        [Required]
        [Range(1, 20)]
        public int Rooms { get; set; }

        [Required]
        [StringLength(200)]
        public string Owner { get; set; } = string.Empty;

        [Required]
        [Range(1000, 10000000)]
        public decimal Price { get; set; }
    }
}
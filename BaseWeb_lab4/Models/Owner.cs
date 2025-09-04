using System.ComponentModel.DataAnnotations;

namespace BaseWeb_lab4.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        // Navigation property for apartments
        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}
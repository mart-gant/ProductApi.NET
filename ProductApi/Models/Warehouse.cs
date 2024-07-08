namespace ProductApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Warehouse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }
    }
}

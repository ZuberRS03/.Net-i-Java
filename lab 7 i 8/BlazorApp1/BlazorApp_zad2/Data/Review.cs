using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorApp_zad2.Data;

namespace BlazorApp_zad2.Data
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [Range(0, 5)]
        public float Rating { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        public Movie? Movie { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}

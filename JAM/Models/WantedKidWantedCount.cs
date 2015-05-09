using System.ComponentModel.DataAnnotations;

namespace JAM.Models
{
    public class WantedKidWantedCount
    {
        [Key]
        public int WantedKidWantedCountId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Beteckning")]
        public string Name { get; set; }
    }
}
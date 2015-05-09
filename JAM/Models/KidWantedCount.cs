using System.ComponentModel.DataAnnotations;

namespace JAM.Models
{
    public class KidWantedCount
    {
        [Key]
        public int KidWantedCountId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Beteckning")]
        public string Name { get; set; }
    }
}
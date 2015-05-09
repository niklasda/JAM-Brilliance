using System.ComponentModel.DataAnnotations;

namespace JAM.Models
{
    public class Referrer
    {
        [Key]
        public int ReferrerId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Beteckning")]
        public string Name { get; set; }
    }
}
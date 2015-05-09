using System.ComponentModel.DataAnnotations;

namespace JAM.Models
{
    public class DatePackage
    {
        [Key]
        public int DatePackageId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Beteckning")]
        public string Name { get; set; }
    }
}
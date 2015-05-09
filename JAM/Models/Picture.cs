using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAM.Models
{
    [Table("Pictures")]
    public class Picture
    {
        [Key]
        public int PictureId { get; set; }

        [Display(Name = "Ladda upp en bild på dig själv (jpeg eller png)")]
        [Column("Picture")]
        public byte[] ThePicture { get; set; }

        public string ContentType { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JAM.Models.ViewModels
{
    public class MyPicturesViewModel
    {
        public IEnumerable<PictureViewModel> Pictures { get; set; }

        [Required]
        [Display(Name = "Ladda upp bild")]
        public int PictureDummyId { get; set; }
    }
}
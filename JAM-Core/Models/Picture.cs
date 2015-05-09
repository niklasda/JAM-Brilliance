using System;
using System.ComponentModel.DataAnnotations;

namespace JAM.Core.Models
{
    public class Picture
    {
        public int PictureId { get; set; }

        public Guid PictureGuid { get; set; }

        public int SurveyId { get; set; }

        public string Name { get; set; }

        public bool IsMain { get; set; }

        public bool IsApproved { get; set; }

        [Display(Name = "Ladda upp en bild på dig själv (jpeg eller png)")]
        public byte[] ThePicture { get; set; }

        public string ContentType { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
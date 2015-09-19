using System;

namespace JAM.Models.ViewModels
{
    public class PictureViewModel
    {
        public int PictureId { get; set; }

        public int SurveyId { get; set; }

        public string Name { get; set; }

        public bool IsMain { get; set; }

        public bool IsApproved { get; set; }

        public string ContentType { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
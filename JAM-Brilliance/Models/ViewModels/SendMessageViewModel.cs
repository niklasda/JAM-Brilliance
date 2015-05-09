using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace JAM.Brilliance.Models.ViewModels
{
    public class SendMessageViewModel
    {
        public int MessageId { get; set; }

        public int FromSurveyId { get; set; }

        public int ToSurveyId { get; set; }

        //// public bool IsDeleted { get; set; }

        public bool IsUnread { get { return ReadDate == DateTime.MinValue; } }

        public DateTime SendDate { get; set; }

        public DateTime ReadDate { get; set; }

        [Required(ErrorMessage = "{0} måste anges")]
        [StringLength(500, ErrorMessage = "{0} får vara max {1} tecken långt.")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Display(Name = "Meddelande")]
        public string Body { get; set; }

        [Display(Name = "Mottagare")]
        public string ToName { get; set; }

        [Display(Name = "Avsändare")]
        public string FromName { get; set; }

        public bool IsFromDisabled { get; set; }

        public bool IsToDisabled { get; set; }

        public bool IsSentMessage { get; set; }

        public bool IsFromAnonymous { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using JAM.Core.Logic;

namespace JAM.Brilliance.Models.ViewModels
{
    public class ShortSurveyViewModel
    {
        public int SurveyId { get; set; }

        public string Name { get; set; }

        [StringLength(250)]
        [UIHint("tinymce_readonly"), AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Profiltext")]
        public string Note1 { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Postnummer")]
        public string PostalCode { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Stad")]
        public string City { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Senaste aktivitet")]
        public DateTime LastActivityDate { get; set; }

        public string LastActivityShortDateString
        {
            get
            {
                if (LastActivityDate.Equals(DateTime.MinValue))
                {
                    return "Aldrig";
                }
                else
                {
                    return LastActivityDate.ToString(Constants.DateTimeFormat);
                }
            }
        }

        public int WhatSearchingForWhatId { get; set; }

        public bool IsMale { get { return WhatSearchingForWhatId == 2 || WhatSearchingForWhatId == 3; } }

        public bool IsDisabled { get; set; }

        [Display(Name = "Längd")]
        public int Height { get; set; }

        [Display(Name = "Vikt")]
        public int Weight { get; set; }

        public DateTime? Birth { get; set; }

        [Display(Name = "Ålder")]
        public int? Age
        {
            get
            {
                if (Birth.HasValue)
                {
                    return DateTime.Today.Year - Birth.Value.Year;
                }

                return null;
            }
        }

        public int? Year
        {
            get
            {
                if (Birth.HasValue)
                {
                    return Birth.Value.Year;
                }

                return null;
            }
        }

        public double Lat { get; set; }

        public double Long { get; set; }
    }
}
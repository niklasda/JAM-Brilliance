using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using JAM.App_GlobalResources;
using JAM.Core.Models.Hard;
using JAM.Validators;

namespace JAM.Models.ViewModels
{
    public class SurveyPage1ViewModel : SurveyViewModelBase
    {
        [Required(ErrorMessage = "{0} måste anges")]
        [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 4)]
        [DataType(DataType.Text)]
        [Display(ResourceType = typeof(Global), Name = "Signup_Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} måste anges")]
        [StringLength(15, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 4)]
        [DataType(DataType.PostalCode)]
        [Display(ResourceType = typeof(Global), Name = "Signup_PostalCode")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "{0} måste anges")]
        [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(ResourceType = typeof(Global), Name = "Signup_City")]
        public string City { get; set; }

        [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(ResourceType = typeof(Global), Name = "Signup_Country")]
        public string Country { get; set; }

        //// [Required(ErrorMessage = "{0} måste anges")]
        //// [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 5)]
        //// [DataType(DataType.EmailAddress)]
        //// [ReadOnly(true)]

        [Display(ResourceType = typeof(Global), Name = "Signup_Email")]
        public string Email { get; set; }

        //// [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 5)]
        //// [DataType(DataType.Text)]
        //// [Display(ResourceType = typeof(SurveyTexts), Name = "Signup_Phone")]
        //// public string Phone { get; set; }
        
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Min profiltext")]
        public string Note1 { get; set; }

        [Display(Name = "Singel", GroupName = "Civilstånd")]
        public bool IsSingle { get; set; }

        [Display(Name = "Separerad")]
        public bool IsSeparated { get; set; }

        [Display(Name = "Skild")]
        public bool IsDivorced { get; set; }

        [Display(Name = "Änka/änkling")]
        public bool IsWidowed { get; set; }

        [Display(Name = "Själv, ofta med släkt och vänner på besök", GroupName = "Bor")]
        public bool LivingAloneWithVisitors { get; set; }

        [Display(Name = "Själv")]
        public bool LivingAlone { get; set; }

        [Display(Name = "Med husdjur")]
        public bool LivingWithPets { get; set; }

        [Display(Name = "Med barn")]
        public bool LivingWithKids { get; set; }

        [Display(Name = "Med föräldrar")]
        public bool LivingWithParents { get; set; }

        [Display(Name = "Med rumskompis(ar)")]
        public bool LivingWithFriends { get; set; }

        [Display(Name = "Med sitt/sina barn del av tiden")]
        public bool LivingWithPartTimeKids { get; set; }

        [Display(ResourceType = typeof(Global), Name = "Signup_Upload")]
        public int PictureDummyId { get; set; }

        [Display(Name = "Har barn")]
        public int KidsCountId { get; set; }

        [Display(Name = "Vill ha barn")]
        public int KidsWantedCountId { get; set; }

        [Display(Name = "Vill ha barn")]
        public int WantedKidsWantedCountId { get; set; }

        [Display(Name = "Europé", GroupName = "Etniskt tillhörighet")]
        public bool IsEthnicNorthEuropean { get; set; }

        [Display(Name = "Latinamerikan")]
        public bool IsEthnicLatin { get; set; }

        [Display(Name = "Mulatt")]
        public bool IsEthnicMulat { get; set; }

        [Display(Name = "Mörkhyad")]
        public bool IsEthnicDark { get; set; }

        [Display(Name = "Asiat")]
        public bool IsEthnicAsian { get; set; }

        [Display(Name = "Kaukasisk")]
        public bool IsEthnicCaucasic { get; set; }

        [Display(Name = "Mellanöstern")]
        public bool IsEthnicMiddleEastern { get; set; }

        [StringLength(150)]
        [Display(Name = "Modersmål")]
        public string MotherLanguage { get; set; }

        [Display(Name = "Slank", GroupName = "Kroppstyp")]
        public bool IsBodySlim { get; set; }

        [Display(Name = "Ganska genomsnittlig")]
        public bool IsBodyAverage { get; set; }

        [Display(Name = "Atletisk/muskulös")]
        public bool IsBodyAthletic { get; set; }

        [Display(Name = "Kraftigt byggt")]
        public bool IsBodyHeavy { get; set; }

        [Display(Name = "Överviktig")]
        public bool IsBodyChubby { get; set; }

        [Display(Name = "Vad söker du")]
        [Range(2, int.MaxValue, ErrorMessage = "Måste välja")]
        public int WhatSearchingForWhatId { get; set; }

        [Required(ErrorMessage = "{0} måste anges")]
        [Display(Name = "Längd", Prompt = "centimeter")]
        [Range(50, 250, ErrorMessage = "{0} måste vara mellan {1} och {2}")]
        public int Height { get; set; }

        [Display(Name = "Vikt", Prompt = "kilo")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "{0} måste anges")]
        [DataType(DataType.Date)]
        [Display(Name = "Födelsedatum", Prompt = "åååå-mm-dd")]
        [AgeValidator]
        public DateTime? Birth { get; set; }

        public IEnumerable<KidWantedCount> KidsWantedCounts { get; set; }

        public IEnumerable<KidCount> KidsCounts { get; set; }

        public IEnumerable<WhatSearchingForWhat> WhatSearchingForWhats { get; set; }
    }
}
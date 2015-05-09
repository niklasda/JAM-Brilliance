using System.ComponentModel.DataAnnotations;

namespace JAM.Brilliance.Models.ViewModels
{
    public class SurveyPage2ViewModel : SurveyViewModelBase
    {
        [Display(Name = "Arbetssökande", GroupName = "Yrke")]
        public bool IsProfNot { get; set; }

        [Display(Name = "Administration/kundtjänst")]
        public bool IsProfAdmin { get; set; }

        [Display(Name = "Artist/konstnär")]
        public bool IsProfArtist { get; set; }

        [Display(Name = "Bygg")]
        public bool IsProfConstruction { get; set; }

        [Display(Name = "HR")]
        public bool IsProfHr { get; set; }

        [Display(Name = "IT/Telekom")]
        public bool IsProfIt { get; set; }

        [Display(Name = "Detaljhandel")]
        public bool IsProfDetail { get; set; }

        [Display(Name = "Egenföretagare")]
        public bool IsProfSelf { get; set; }

        [Display(Name = "Ekonomi/finans")]
        public bool IsProfFinance { get; set; }

        [Display(Name = "Försäljning/marknadsföring")]
        public bool IsProfSales { get; set; }

        [Display(Name = "Hälsovård/sjukvård")]
        public bool IsProfHealth { get; set; }

        [Display(Name = "Hotell/restaurant/turism")]
        public bool IsProfHotel { get; set; }

        [Display(Name = "Juridik")]
        public bool IsProfLaw { get; set; }

        [Display(Name = "Reklam/media/underhållning")]
        public bool IsProfAdvertising { get; set; }

        [Display(Name = "Myndigheter")]
        public bool IsProfState { get; set; }

        [Display(Name = "Utbildning")]
        public bool IsProfEducation { get; set; }

        [Display(Name = "Pensionär")]
        public bool IsProfRetired { get; set; }

        [Display(Name = "Studerande")]
        public bool IsProfStudent { get; set; }

        [Display(Name = "Grundskola", GroupName = "Utbildning")]
        public bool IsEduBasic { get; set; }

        [Display(Name = "Gymnasium")]
        public bool IsEduCollage { get; set; }

        [Display(Name = "Högskola/Universitet")]
        public bool IsEduUniversity { get; set; }

        [Display(Name = "Kandidat")]
        public bool IsEduKand { get; set; }

        [Display(Name = "Magister")]
        public bool IsEduMag { get; set; }

        [Display(Name = "Doktor")]
        public bool IsEduDoc { get; set; }

        [Display(Name = "Autodidakt")]
        public bool IsEduLife { get; set; }

        [Display(Name = "Blont", GroupName = "Hårfärg")]
        public bool IsHairBlond { get; set; }

        [Display(Name = "Rött")]
        public bool IsHairRed { get; set; }

        [Display(Name = "Brunt")]
        public bool IsHairBrown { get; set; }

        [Display(Name = "Vitt/grått")]
        public bool IsHairGrey { get; set; }

        [Display(Name = "Svart")]
        public bool IsHairBlack { get; set; }

        [Display(Name = "Rakad")]
        public bool IsHairBald { get; set; }

        [Display(Name = "Rakat", GroupName = "Hårlängd")]
        public bool IsHairLengthShaved { get; set; }

        [Display(Name = "Väldigt kort")]
        public bool IsHairLengthVeryShort { get; set; }

        [Display(Name = "Kort")]
        public bool IsHairLengthShort { get; set; }

        [Display(Name = "Axellångt")]
        public bool IsHairLengthShoulder { get; set; }

        [Display(Name = "Långt")]
        public bool IsHairLengthLong { get; set; }

        [Display(Name = "Mycket långt")]
        public bool IsHairLengthVeryLong { get; set; }

        //// [Display(Name = "Flintskallig")]
        //// public bool IsHairLengthBald { get; set; }
        
        [Display(Name = "Blå", GroupName = "Ögonfärg")]
        public bool IsEyesBlue { get; set; }

        [Display(Name = "Bruna")]
        public bool IsEyesBrown { get; set; }

        [Display(Name = "Grå")]
        public bool IsEyesGrey { get; set; }

        [Display(Name = "Gröna")]
        public bool IsEyesGreen { get; set; }

        [Display(Name = "Melerade")]
        public bool IsEyesMix { get; set; }

        //// Lifestyle

        [Display(Name = "Äventyrlig", GroupName = "Personlighet")]
        public bool IsPersonAdventurous { get; set; }

        [Display(Name = "Bekymmerslös")]
        public bool IsPersonCarefree { get; set; }

        [Display(Name = "Blyg")]
        public bool IsPersonShy { get; set; }

        [Display(Name = "Dominerande")]
        public bool IsPersonDominant { get; set; }

        [Display(Name = "Ensamvarg")]
        public bool IsPersonLoner { get; set; }

        [Display(Name = "Envis")]
        public bool IsPersonStubborn { get; set; }

        [Display(Name = "Generös")]
        public bool IsPersonGenerous { get; set; }

        [Display(Name = "Glad")]
        public bool IsPersonHappy { get; set; }

        [Display(Name = "Humoristisk")]
        public bool IsPersonHomorous { get; set; }

        [Display(Name = "Känslig")]
        public bool IsPersonSensitive { get; set; }

        [Display(Name = "Ambitiös")]
        public bool IsPersonAmbitious { get; set; }

        [Display(Name = "Analytisk")]
        public bool IsPersonAnalytic { get; set; }

        [Display(Name = "Tempramentsfull/passionerad")]
        public bool IsPersonTemper { get; set; }

        [Display(Name = "Livfull")]
        public bool IsPersonLivly { get; set; }

        [Display(Name = "Lugn")]
        public bool IsPersonCalm { get; set; }

        [Display(Name = "Rastlös")]
        public bool IsPersonRestless { get; set; }

        [Display(Name = "Omtänksam")]
        public bool IsPersonConsidering { get; set; }

        [Display(Name = "Reserverad")]
        public bool IsPersonReserved { get; set; }

        [Display(Name = "Social")]
        public bool IsPersonSocial { get; set; }

        [Display(Name = "Spontan")]
        public bool IsPersonSpantanious { get; set; }

        [Display(Name = "Omhändertagande")]
        public bool IsPersonCaring { get; set; }

        [Display(Name = "Inspirerande")]
        public bool IsPersonInspiring { get; set; }

        [Display(Name = "Trygg")]
        public bool IsPersonSafe { get; set; }

        [Display(Name = "Strukturerad/planerad")]
        public bool IsPersonStructured { get; set; }
    }
}
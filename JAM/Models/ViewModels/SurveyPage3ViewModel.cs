using System.ComponentModel.DataAnnotations;

namespace JAM.Models.ViewModels
{
    public class SurveyPage3ViewModel : SurveyViewModelBase
    {
        [Display(Name = "Adventist", GroupName = "Religion")]
        public bool IsRelAdventist { get; set; }

        [Display(Name = "Agnostiker")]
        public bool IsRelAgnostic { get; set; }

        [Display(Name = "Andlig")]
        public bool IsRelSpiritual { get; set; }

        [Display(Name = "Anglikan")]
        public bool IsRelAnglican { get; set; }

        [Display(Name = "Ateist")]
        public bool IsRelAtheist { get; set; }

        [Display(Name = "Baptist")]
        public bool IsRelBaptist { get; set; }

        [Display(Name = "Hindu")]
        public bool IsRelHindu { get; set; }

        [Display(Name = "Buddhist")]
        public bool IsRelBuddhist { get; set; }

        [Display(Name = "Judisk")]
        public bool IsRelJewish { get; set; }

        [Display(Name = "Katolik")]
        public bool IsRelCatholic { get; set; }

        [Display(Name = "Kristen")]
        public bool IsRelChistian { get; set; }

        [Display(Name = "Metodist")]
        public bool IsRelMethodist { get; set; }

        [Display(Name = "Mormon")]
        public bool IsRelMormon { get; set; }

        [Display(Name = "Muslim")]
        public bool IsRelMuslim { get; set; }

        [Display(Name = "Ortodox")]
        public bool IsRelOrtodox { get; set; }

        [Display(Name = "Pingstvän")]
        public bool IsRelPingst { get; set; }

        [Display(Name = "Protestant")]
        public bool IsRelProtestant { get; set; }

        [Display(Name = "Taoism")]
        public bool IsRelTao { get; set; }

        [Display(Name = "Annat")]
        public bool IsRelOther { get; set; }

        [Display(Name = "Praktiserande", GroupName = "Religionsutövning")]
        public bool IsRelPracticing { get; set; }

        [Display(Name = "Ibland praktiserande")]
        public bool IsRelSomewhat { get; set; }

        [Display(Name = "Icke praktiserande")]
        public bool IsRelNotPracticing { get; set; }

        [Display(Name = "mindre än 250 000", GroupName = "Inkomst")]
        public bool IsIncomeLess { get; set; }

        [Display(Name = "250 000-500 000")]
        public bool IsIncome1 { get; set; }

        [Display(Name = "500 001-1 000 000")]
        public bool IsIncome2 { get; set; }

        [Display(Name = "1 000 001-1 500 000")]
        public bool IsIncome3 { get; set; }

        [Display(Name = "1 500 001-2 000 000")]
        public bool IsIncome4 { get; set; }

        [Display(Name = "2 000 001-2 500 000")]
        public bool IsIncome5 { get; set; }

        [Display(Name = "Över 2 500 000")]
        public bool IsIncomeMore { get; set; }

        [Display(Name = "Försöker sluta", GroupName = "Tobaksvanor")]
        public bool IsTobacQuitting { get; set; }

        [Display(Name = "Röker cigarr/pipa")]
        public bool IsTobacCigar { get; set; }

        [Display(Name = "Röker regelbundet")]
        public bool IsTobacRegularly { get; set; }

        [Display(Name = "Feströker")]
        public bool IsTobacParty { get; set; }

        [Display(Name = "Snusar")]
        public bool IsTobacSnus { get; set; }

        [Display(Name = "Röker/snusar inte")]
        public bool IsTobacNot { get; set; }

        [Display(Name = "Dricker inte", GroupName = "Alkoholvanor")]
        public bool IsAlcNot { get; set; }

        [Display(Name = "Dricker sällan")]
        public bool IsAlcLittle { get; set; }

        [Display(Name = "Dricker i sociala sammanhang")]
        public bool IsAlcSocially { get; set; }

        [Display(Name = "Dricker flera gånger per vecka")]
        public bool IsAlcMuch { get; set; }

        //// Looks

        [Display(Name = "Bröstet", GroupName = "Mest nöjd med")]
        public bool IsPleWiChest { get; set; }

        [Display(Name = "Armarna")]
        public bool IsPleWiArms { get; set; }

        [Display(Name = "Benen")]
        public bool IsPleWiLegs { get; set; }

        [Display(Name = "Fötterna")]
        public bool IsPleWiFeet { get; set; }

        [Display(Name = "Halsen")]
        public bool IsPleWiNeck { get; set; }

        [Display(Name = "Händerna")]
        public bool IsPleWiHands { get; set; }

        [Display(Name = "Håret")]
        public bool IsPleWiHair { get; set; }

        [Display(Name = "Leendet")]
        public bool IsPleWiSmile { get; set; }

        [Display(Name = "Läpparna")]
        public bool IsPleWiLips { get; set; }

        [Display(Name = "Muskler")]
        public bool IsPleWiMuscles { get; set; }

        [Display(Name = "Magen")]
        public bool IsPleWiBelly { get; set; }

        [Display(Name = "Öronen")]
        public bool IsPleWiEars { get; set; }

        [Display(Name = "Ögonen")]
        public bool IsPleWiEyes { get; set; }

        [Display(Name = "Rumpan")]
        public bool IsPleWiButt { get; set; }

        [Display(Name = "Vaderna")]
        public bool IsPleWiCalf { get; set; }

        [Display(Name = "Tränar inte", GroupName = "Tränar")]
        public bool IsExcerNot { get; set; }

        [Display(Name = "Tränar ibland")]
        public bool IsExcerSome { get; set; }

        [Display(Name = "Tränar regelbundet")]
        public bool IsExcerRegular { get; set; }

        [Display(Name = "Träningsfanatiker")]
        public bool IsExcerAlways { get; set; }
    }
}
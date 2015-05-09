using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JAM.Validators;

namespace JAM.Models
{
    [Table("Surveys")]
    public class Survey
    {
        [Key]
        public int SurveyId { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsInterviewed { get; set; }

        public bool IsPaid { get; set; }

        [Required(ErrorMessage = "Namn måste anges")]
        [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 4)]
        [DataType(DataType.Text)]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 4)]
        [DataType(DataType.Text)]
        [Display(Name = "Gatuadress")]
        public string Street { get; set; }

        [StringLength(15, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 4)]
        [DataType(DataType.Text)]
        [Display(Name = "Postnummer")]
        public string PostalCode { get; set; }

        [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Stad")]
        public string City { get; set; }

        [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Land")]
        public string Country { get; set; }

        [Required(ErrorMessage = "E-Post måste anges")]
        [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 5)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "E-post verkar inte ha korrekt format")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Post")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon måste anges")]
        [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

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

        public virtual WantedSurvey WantedSurvey { get; set; }

        public virtual Picture Picture { get; set; }

        [Display(Name = "Har barn")]
        [ForeignKey("KidsCount")]
        public int KidsCountId { get; set; }

        public virtual KidCount KidsCount { get; set; }

        [Display(Name = "Vill ha barn")]
        [ForeignKey("KidsWantedCount")]
        public int KidsWantedCountId { get; set; }

        public virtual KidWantedCount KidsWantedCount { get; set; }

        [Display(Name = "Vill ha barn")]
        [ForeignKey("WantedKidsWantedCount")]
        public int WantedKidsWantedCountId { get; set; }

        public virtual WantedKidWantedCount WantedKidsWantedCount { get; set; }

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

        [Display(Name = "Kön")]
        [Required(ErrorMessage = "Kön måste anges")]
        public bool IsMale { get; set; }

        // [Required(ErrorMessage = "Längd måste anges")]
        [Display(Name = "Längd", Prompt = "centimeter")]

        // [Range(100, 250, ErrorMessage = "{0} måste vara mellan {1} och {2}")]
        public int Height { get; set; }

        // [Required(ErrorMessage = "Vikt måste anges")]
        [Display(Name = "Vikt", Prompt = "kilo")]

        // [Range(40, 399, ErrorMessage = "{0} måste vara mellan {1} och {2}")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "Födelsedatum måste anges")]
        [DataType(DataType.Date)]
        [Display(Name = "Födelsedatum", Prompt = "åååå-mm-dd")]
        [AgeValidator]
        public DateTime Birth { get; set; }

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

        [Display(Name = "Bohemisk", GroupName = "Klädstil")]
        public bool IsDressBoheme { get; set; }

        [Display(Name = "Business")]
        public bool IsDressBusiness { get; set; }

        [Display(Name = "Etnisk")]
        public bool IsDressEthnic { get; set; }

        [Display(Name = "Klassisk")]
        public bool IsDressClassic { get; set; }

        [Display(Name = "Modemedveten")]
        public bool IsDressFashion { get; set; }

        [Display(Name = "Rockig")]
        public bool IsDressRock { get; set; }

        [Display(Name = "Sofistikerad")]
        public bool IsDressSophisticated { get; set; }

        [Display(Name = "Sportig")]
        public bool IsDressSport { get; set; }

        [Display(Name = "Trendig")]
        public bool IsDressTrend { get; set; }

        [Display(Name = "Absolut inte", GroupName = "Kroppsdekorationer")]
        public bool IsBodyArtNever { get; set; }

        [Display(Name = "Synlig tatuering")]
        public bool IsBodyArtVisible { get; set; }

        [Display(Name = "Diskret tatuering")]
        public bool IsBodyArtHidden { get; set; }

        [Display(Name = "Piercing")]
        public bool IsBodyArtPiercing { get; set; }

        [Display(Name = "Intimpiercing")]
        public bool IsBodyArtAsk { get; set; }

        //-- Lifestyle

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

        [Display(Name = "Väldigt romantisk", GroupName = "Romantisk")]
        public bool IsRomanticVery { get; set; }

        [Display(Name = "Romantisk")]
        public bool IsRomantic { get; set; }

        [Display(Name = "Inte särskilt romantisk")]
        public bool IsRomanticLittle { get; set; }

        [Display(Name = "Inte romantisk alls")]
        public bool IsRomanticNot { get; set; }

        [Display(Name = "Heligt", GroupName = "Syn på äktenskapet")]
        public bool IsMarriageHoly { get; set; }

        [Display(Name = "Väldigt viktigt")]
        public bool IsMarriageVeryImportant { get; set; }

        [Display(Name = "Viktigt")]
        public bool IsMarriageImportant { get; set; }

        [Display(Name = "Inte så viktigt")]
        public bool IsMarriageLessImportant { get; set; }

        [Display(Name = "Oviktigt")]
        public bool IsMarriageUnImportant { get; set; }

        [Display(Name = "Gör inte om det")]
        public bool IsMarriageNotAgain { get; set; }

        [Display(Name = "Ultrakonservativ", GroupName = "Politisk uppfattning")]
        public bool IsPoliticVeryConservative { get; set; }

        [Display(Name = "Konservativ")]
        public bool IsPoliticConservative { get; set; }

        [Display(Name = "Mitt emellan")]
        public bool IsPoliticMiddle { get; set; }

        [Display(Name = "Liberal")]
        public bool IsPoliticLiberal { get; set; }

        [Display(Name = "Mycket liberal")]
        public bool IsPoliticVeryLiberal { get; set; }

        [Display(Name = "Nonkonformist")]
        public bool IsPoliticNonConformist { get; set; }

        [Display(Name = "Annan ståndpunkt")]
        public bool IsPoliticOther { get; set; }

        [Display(Name = "New age", GroupName = "Favoritmusik")]
        public bool IsMusicNewAge { get; set; }

        [Display(Name = "Blues")]
        public bool IsMusicBlues { get; set; }

        [Display(Name = "Country")]
        public bool IsMusicCountry { get; set; }

        [Display(Name = "Hitlistan")]
        public bool IsMusicHits { get; set; }

        [Display(Name = "Disco")]
        public bool IsMusicDisco { get; set; }

        [Display(Name = "Elektronika")]
        public bool IsMusicElectronica { get; set; }

        [Display(Name = "Etnisk musik")]
        public bool IsMusicEthnic { get; set; }

        [Display(Name = "Filmmusik")]
        public bool IsMusicFilm { get; set; }

        [Display(Name = "Folkmusik")]
        public bool IsMusicFolk { get; set; }

        [Display(Name = "Funk")]
        public bool IsMusicFunk { get; set; }

        [Display(Name = "Andlig/gospel")]
        public bool IsMusicGospel { get; set; }

        [Display(Name = "Hårdrock")]
        public bool IsMusicMetal { get; set; }

        [Display(Name = "Hip-hop")]
        public bool IsMusicHipHop { get; set; }

        [Display(Name = "Jazz")]
        public bool IsMusicJazz { get; set; }

        [Display(Name = "Klassisk")]
        public bool IsMusicClassic { get; set; }

        [Display(Name = "Latinsk")]
        public bool IsMusicLatin { get; set; }

        [Display(Name = "Musikal")]
        public bool IsMusicMusical { get; set; }

        [Display(Name = "Opera")]
        public bool IsMusicOpera { get; set; }

        [Display(Name = "Pop")]
        public bool IsMusicPop { get; set; }

        [Display(Name = "R'n'B")]
        public bool IsMusicRnB { get; set; }

        [Display(Name = "Rock")]
        public bool IsMusicRock { get; set; }

        [Display(Name = "Reggae")]
        public bool IsMusicReggae { get; set; }

        [Display(Name = "Soul")]
        public bool IsMusicSoul { get; set; }

        [Display(Name = "Synth")]
        public bool IsMusicSynth { get; set; }

        [Display(Name = "Action", GroupName = "Favoritfilmer")]
        public bool IsMovieAction { get; set; }

        [Display(Name = "Animerat")]
        public bool IsMovieAnimation { get; set; }

        [Display(Name = "Äventyr")]
        public bool IsMovieAdventure { get; set; }

        [Display(Name = "Dokumentär")]
        public bool IsMovieDocumentary { get; set; }

        [Display(Name = "Drama")]
        public bool IsMovieDrama { get; set; }

        [Display(Name = "Erotik")]
        public bool IsMovieErotic { get; set; }

        [Display(Name = "Fantasy")]
        public bool IsMovieFantasy { get; set; }

        [Display(Name = "Historik")]
        public bool IsMovieHistoric { get; set; }

        [Display(Name = "Independent")]
        public bool IsMovieIndependent { get; set; }

        [Display(Name = "Komedi")]
        public bool IsMovieComedy { get; set; }

        [Display(Name = "Kortfilm")]
        public bool IsMovieShort { get; set; }

        [Display(Name = "Krigsfilm")]
        public bool IsMovieWar { get; set; }

        [Display(Name = "Kriminaldrama")]
        public bool IsMovieCrime { get; set; }

        [Display(Name = "Manga")]
        public bool IsMovieManga { get; set; }

        [Display(Name = "Musik")]
        public bool IsMovieMusic { get; set; }

        [Display(Name = "Romantisk komedi")]
        public bool IsMovieRomantic { get; set; }

        [Display(Name = "Rysare")]
        public bool IsMovieThriller { get; set; }

        [Display(Name = "Science fiction")]
        public bool IsMovieSciFi { get; set; }

        [Display(Name = "Skräck")]
        public bool IsMovieHorror { get; set; }

        [Display(Name = "Tecknat")]
        public bool IsMovieCartoon { get; set; }

        [Display(Name = "Western")]
        public bool IsMovieWestern { get; set; }

        [Display(Name = "Astrologi/New age", GroupName = "Intressen")]
        public bool IsHobbyNewAge { get; set; }

        [Display(Name = "Bilar/motorcyklar")]
        public bool IsHobbyCars { get; set; }

        [Display(Name = "Dans")]
        public bool IsHobbyDance { get; set; }

        [Display(Name = "Djur")]
        public bool IsHobbyAnimals { get; set; }

        [Display(Name = "Film/bio")]
        public bool IsHobbyMovies { get; set; }

        [Display(Name = "Fiske/jakt")]
        public bool IsHobbyHunting { get; set; }

        [Display(Name = "Heminredning")]
        public bool IsHobbyInterior { get; set; }

        [Display(Name = "Fotografering")]
        public bool IsHobbyPhoto { get; set; }

        [Display(Name = "Friluftsliv")]
        public bool IsHobbyOutdoors { get; set; }

        [Display(Name = "Konst")]
        public bool IsHobbyArt { get; set; }

        [Display(Name = "Spela kort/brädspel")]
        public bool IsHobbyGame { get; set; }

        [Display(Name = "Matlagning")]
        public bool IsHobbyCooking { get; set; }

        [Display(Name = "Böcker/läsning")]
        public bool IsHobbyReading { get; set; }

        [Display(Name = "Litteratur/historia")]
        public bool IsHobbyHistory { get; set; }

        [Display(Name = "Måla")]
        public bool IsHobbyPainting { get; set; }

        [Display(Name = "Muséer/utställningar")]
        public bool IsHobbyMuseum { get; set; }

        [Display(Name = "Musik")]
        public bool IsHobbyMusic { get; set; }

        [Display(Name = "Politik")]
        public bool IsHobbyPolitics { get; set; }

        [Display(Name = "Resor")]
        public bool IsHobbyTravel { get; set; }

        [Display(Name = "Rollspel")]
        public bool IsHobbyRolePlaying { get; set; }

        [Display(Name = "Shopping")]
        public bool IsHobbyShopping { get; set; }

        [Display(Name = "Sjunga/spela instrument")]
        public bool IsHobbySinging { get; set; }

        [Display(Name = "Skriva")]
        public bool IsHobbyWriting { get; set; }

        [Display(Name = "Spel")]
        public bool IsHobbyGaming { get; set; }

        [Display(Name = "Träning")]
        public bool IsHobbyWorkout { get; set; }

        [Display(Name = "Teater")]
        public bool IsHobbyDrama { get; set; }

        [Display(Name = "Teckna")]
        public bool IsHobbyDrawing { get; set; }

        [Display(Name = "Trädgårdsarbeta")]
        public bool IsHobbyGardening { get; set; }

        [Display(Name = "TV/dataspel")]
        public bool IsHobbyTv { get; set; }

        [Display(Name = "Föreningsverksamhet")]
        public bool IsHobbyCircles { get; set; }

        [Display(Name = "Vandring")]
        public bool IsHobbyHiking { get; set; }

        [Display(Name = "Vinprovning")]
        public bool IsHobbyWine { get; set; }

        [Display(Name = "Mycket aktiviteter", GroupName = "Hur ser en helg ut?")]
        public bool IsWeekendActive { get; set; }

        [Display(Name = "Återhämtning/lugn")]
        public bool IsWeekendChill { get; set; }

        [Display(Name = "Spontan")]
        public bool IsWeekendRandom { get; set; }

        [Display(Name = "Välplanerad")]
        public bool IsWeekendPlanned { get; set; }

        [Display(Name = "Aerobics/dans", GroupName = "Idrottsintressen")]
        public bool IsSportDance { get; set; }

        [Display(Name = "Bollsport")]
        public bool IsSportBalls { get; set; }

        [Display(Name = "Kampsport")]
        public bool IsSportMartial { get; set; }

        [Display(Name = "Fitness/gym")]
        public bool IsSportFitness { get; set; }

        [Display(Name = "Äventyr")]
        public bool IsSportAdventure { get; set; }

        [Display(Name = "Golf")]
        public bool IsSportGolf { get; set; }

        [Display(Name = "Löpning")]
        public bool IsSportRunning { get; set; }

        [Display(Name = "Ridning/hästar")]
        public bool IsSportRiding { get; set; }

        [Display(Name = "Segling/båtsport")]
        public bool IsSportSailing { get; set; }

        [Display(Name = "Skateboard")]
        public bool IsSportSkating { get; set; }

        [Display(Name = "Vindsurfing")]
        public bool IsSportSurfing { get; set; }

        [Display(Name = "Yoga/meditation")]
        public bool IsSportYoga { get; set; }

        [Display(Name = "Racketsport")]
        public bool IsSportRaquet { get; set; }

        [Display(Name = "Tränar inte", GroupName = "Tränar")]
        public bool IsExcerNot { get; set; }

        [Display(Name = "Tränar ibland")]
        public bool IsExcerSome { get; set; }

        [Display(Name = "Tränar regelbundet")]
        public bool IsExcerRegular { get; set; }

        [Display(Name = "Träningsfanatiker")]
        public bool IsExcerAlways { get; set; }

        [Display(Name = "Frukostdejt", GroupName = "Din favoritdejt")]
        public bool IsDateBreakfast { get; set; }

        [Display(Name = "Lunchdejt")]
        public bool IsDateLunch { get; set; }

        [Display(Name = "Romantisk middag")]
        public bool IsDateDinner { get; set; }

        [Display(Name = "Bio")]
        public bool IsDateMovie { get; set; }

        [Display(Name = "Casual matbit på en bar")]
        public bool IsDateBarSnack { get; set; }

        [Display(Name = "Drink")]
        public bool IsDateDrinks { get; set; }

        [Display(Name = "Picnic")]
        public bool IsDatePicnic { get; set; }

        [Display(Name = "Middag i hemmet")]
        public bool IsDateDinnerHome { get; set; }

        [Display(Name = "Fika")]
        public bool IsDateFika { get; set; }

        [Display(Name = "Mycket viktigt", GroupName = "Vikten av utseende")]
        public bool IsLooksImportant { get; set; }

        [Display(Name = "Mindre viktigt")]
        public bool IsLooksLessImportant { get; set; }

        [Display(Name = "Vill göra det mesta tillsammans", GroupName = "Min syn på en kärleksrelation")]
        public bool IsRelationTogether { get; set; }

        [Display(Name = "Värdesätter att man har 'ett eget liv' trots att man är i relation")]
        public bool IsRelationOwnTime { get; set; }

        [Display(Name = "Föredrar äktenskap")]
        public bool IsRelationMarriage { get; set; }

        [Display(Name = "Föredrar samboskap")]
        public bool IsRelationLiving { get; set; }

        [Display(Name = "Föredrar särboskap")]
        public bool IsRelationNotLiving { get; set; }

        [Display(Name = "Välj typ av medlemskap")]
        [ForeignKey("DatePackage")]
        public int DatePackageId { get; set; }

        public virtual DatePackage DatePackage { get; set; }

        [Display(Name = "Hur kom du i kontakt med J&A Matchmaking?")]
        [ForeignKey("Referrer")]
        public int ReferrerId { get; set; }

        public virtual Referrer Referrer { get; set; }

        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Anteckningar")]
        public string Note1 { get; set; }

        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Fler Anteckningar")]
        public string Note2 { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace JAM.Models.ViewModels
{
    public class SurveyPage4ViewModel : SurveyViewModelBase
    {
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

        ////-- Lifestyle

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
    }
}
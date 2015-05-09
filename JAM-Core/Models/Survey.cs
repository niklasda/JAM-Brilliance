using System;

using JAM.Core.Logic;

namespace JAM.Core.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }

        public Guid SurveyGuid { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsInterviewed { get; set; }

        public bool IsPaid { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsSingle { get; set; }

        public bool IsSeparated { get; set; }

        public bool IsDivorced { get; set; }

        public bool IsWidowed { get; set; }

        public bool LivingAloneWithVisitors { get; set; }

        public bool LivingAlone { get; set; }

        public bool LivingWithPets { get; set; }

        public bool LivingWithKids { get; set; }

        public bool LivingWithParents { get; set; }

        public bool LivingWithFriends { get; set; }

        public bool LivingWithPartTimeKids { get; set; }

        public int WantedSurveyId { get; set; }

        public int WhatSearchingForWhatId { get; set; }

        public int KidsCountId { get; set; }

        public int KidsWantedCountId { get; set; }

        public int WantedKidsWantedCountId { get; set; }

        public bool IsEthnicNorthEuropean { get; set; }

        public bool IsEthnicLatin { get; set; }

        public bool IsEthnicMulat { get; set; }

        public bool IsEthnicDark { get; set; }

        public bool IsEthnicAsian { get; set; }

        public bool IsEthnicCaucasic { get; set; }

        public bool IsEthnicMiddleEastern { get; set; }

        public string MotherLanguage { get; set; }

        public bool IsRelAdventist { get; set; }

        public bool IsRelAgnostic { get; set; }

        public bool IsRelSpiritual { get; set; }

        public bool IsRelAnglican { get; set; }

        public bool IsRelAtheist { get; set; }

        public bool IsRelBaptist { get; set; }

        public bool IsRelHindu { get; set; }

        public bool IsRelBuddhist { get; set; }

        public bool IsRelJewish { get; set; }

        public bool IsRelCatholic { get; set; }

        public bool IsRelChistian { get; set; }

        public bool IsRelMethodist { get; set; }

        public bool IsRelMormon { get; set; }

        public bool IsRelMuslim { get; set; }

        public bool IsRelOrtodox { get; set; }

        public bool IsRelPingst { get; set; }

        public bool IsRelProtestant { get; set; }

        public bool IsRelTao { get; set; }

        public bool IsRelOther { get; set; }

        public bool IsRelPracticing { get; set; }

        public bool IsRelSomewhat { get; set; }

        public bool IsRelNotPracticing { get; set; }

        public bool IsProfNot { get; set; }

        public bool IsProfAdmin { get; set; }

        public bool IsProfArtist { get; set; }

        public bool IsProfConstruction { get; set; }

        public bool IsProfHr { get; set; }

        public bool IsProfIt { get; set; }

        public bool IsProfDetail { get; set; }

        public bool IsProfSelf { get; set; }

        public bool IsProfFinance { get; set; }

        public bool IsProfSales { get; set; }

        public bool IsProfHealth { get; set; }

        public bool IsProfHotel { get; set; }

        public bool IsProfLaw { get; set; }

        public bool IsProfAdvertising { get; set; }

        public bool IsProfState { get; set; }

        public bool IsProfEducation { get; set; }

        public bool IsProfRetired { get; set; }

        public bool IsProfStudent { get; set; }

        public bool IsEduBasic { get; set; }

        public bool IsEduCollage { get; set; }

        public bool IsEduUniversity { get; set; }

        public bool IsEduKand { get; set; }

        public bool IsEduMag { get; set; }

        public bool IsEduDoc { get; set; }

        public bool IsEduLife { get; set; }

        public bool IsIncomeLess { get; set; }

        public bool IsIncome1 { get; set; }

        public bool IsIncome2 { get; set; }

        public bool IsIncome3 { get; set; }

        public bool IsIncome4 { get; set; }

        public bool IsIncome5 { get; set; }

        public bool IsIncomeMore { get; set; }

        public bool IsTobacQuitting { get; set; }

        public bool IsTobacCigar { get; set; }

        public bool IsTobacRegularly { get; set; }

        public bool IsTobacParty { get; set; }

        public bool IsTobacSnus { get; set; }

        public bool IsTobacNot { get; set; }

        public bool IsAlcNot { get; set; }

        public bool IsAlcLittle { get; set; }

        public bool IsAlcSocially { get; set; }

        public bool IsAlcMuch { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public DateTime? Birth { get; set; }

        public bool IsBodySlim { get; set; }

        public bool IsBodyAverage { get; set; }

        public bool IsBodyAthletic { get; set; }

        public bool IsBodyHeavy { get; set; }

        public bool IsBodyChubby { get; set; }

        public bool IsHairBlond { get; set; }

        public bool IsHairRed { get; set; }

        public bool IsHairBrown { get; set; }

        public bool IsHairGrey { get; set; }

        public bool IsHairBlack { get; set; }

        public bool IsHairBald { get; set; }

        public bool IsHairLengthShaved { get; set; }

        public bool IsHairLengthVeryShort { get; set; }

        public bool IsHairLengthShort { get; set; }

        public bool IsHairLengthShoulder { get; set; }

        public bool IsHairLengthLong { get; set; }

        public bool IsHairLengthVeryLong { get; set; }

        public bool IsEyesBlue { get; set; }

        public bool IsEyesBrown { get; set; }

        public bool IsEyesGrey { get; set; }

        public bool IsEyesGreen { get; set; }

        public bool IsEyesMix { get; set; }

        public bool IsPleWiChest { get; set; }

        public bool IsPleWiArms { get; set; }

        public bool IsPleWiLegs { get; set; }

        public bool IsPleWiFeet { get; set; }

        public bool IsPleWiNeck { get; set; }

        public bool IsPleWiHands { get; set; }

        public bool IsPleWiHair { get; set; }

        public bool IsPleWiSmile { get; set; }

        public bool IsPleWiLips { get; set; }

        public bool IsPleWiMuscles { get; set; }

        public bool IsPleWiBelly { get; set; }

        public bool IsPleWiEars { get; set; }

        public bool IsPleWiEyes { get; set; }

        public bool IsPleWiButt { get; set; }

        public bool IsPleWiCalf { get; set; }

        public bool IsDressBoheme { get; set; }

        public bool IsDressBusiness { get; set; }

        public bool IsDressEthnic { get; set; }

        public bool IsDressClassic { get; set; }

        public bool IsDressFashion { get; set; }

        public bool IsDressRock { get; set; }

        public bool IsDressSophisticated { get; set; }

        public bool IsDressSport { get; set; }

        public bool IsDressTrend { get; set; }

        public bool IsBodyArtNever { get; set; }

        public bool IsBodyArtVisible { get; set; }

        public bool IsBodyArtHidden { get; set; }

        public bool IsBodyArtPiercing { get; set; }

        public bool IsBodyArtAsk { get; set; }

        public bool IsPersonAdventurous { get; set; }

        public bool IsPersonCarefree { get; set; }

        public bool IsPersonShy { get; set; }

        public bool IsPersonDominant { get; set; }

        public bool IsPersonLoner { get; set; }

        public bool IsPersonStubborn { get; set; }

        public bool IsPersonGenerous { get; set; }

        public bool IsPersonHappy { get; set; }

        public bool IsPersonHomorous { get; set; }

        public bool IsPersonSensitive { get; set; }

        public bool IsPersonAmbitious { get; set; }

        public bool IsPersonAnalytic { get; set; }

        public bool IsPersonTemper { get; set; }

        public bool IsPersonLivly { get; set; }

        public bool IsPersonCalm { get; set; }

        public bool IsPersonRestless { get; set; }

        public bool IsPersonConsidering { get; set; }

        public bool IsPersonReserved { get; set; }

        public bool IsPersonSocial { get; set; }

        public bool IsPersonSpantanious { get; set; }

        public bool IsPersonCaring { get; set; }

        public bool IsPersonInspiring { get; set; }

        public bool IsPersonSafe { get; set; }

        public bool IsPersonStructured { get; set; }

        public bool IsRomanticVery { get; set; }

        public bool IsRomantic { get; set; }

        public bool IsRomanticLittle { get; set; }

        public bool IsRomanticNot { get; set; }

        public bool IsMarriageHoly { get; set; }

        public bool IsMarriageVeryImportant { get; set; }

        public bool IsMarriageImportant { get; set; }

        public bool IsMarriageLessImportant { get; set; }

        public bool IsMarriageUnImportant { get; set; }

        public bool IsMarriageNotAgain { get; set; }

        public bool IsPoliticVeryConservative { get; set; }

        public bool IsPoliticConservative { get; set; }

        public bool IsPoliticMiddle { get; set; }

        public bool IsPoliticLiberal { get; set; }

        public bool IsPoliticVeryLiberal { get; set; }

        public bool IsPoliticNonConformist { get; set; }

        public bool IsPoliticOther { get; set; }

        public bool IsMusicNewAge { get; set; }

        public bool IsMusicBlues { get; set; }

        public bool IsMusicCountry { get; set; }

        public bool IsMusicHits { get; set; }

        public bool IsMusicDisco { get; set; }

        public bool IsMusicElectronica { get; set; }

        public bool IsMusicEthnic { get; set; }

        public bool IsMusicFilm { get; set; }

        public bool IsMusicFolk { get; set; }

        public bool IsMusicFunk { get; set; }

        public bool IsMusicGospel { get; set; }

        public bool IsMusicMetal { get; set; }

        public bool IsMusicHipHop { get; set; }

        public bool IsMusicJazz { get; set; }

        public bool IsMusicClassic { get; set; }

        public bool IsMusicLatin { get; set; }

        public bool IsMusicMusical { get; set; }

        public bool IsMusicOpera { get; set; }

        public bool IsMusicPop { get; set; }

        public bool IsMusicRnB { get; set; }

        public bool IsMusicRock { get; set; }

        public bool IsMusicReggae { get; set; }

        public bool IsMusicSoul { get; set; }

        public bool IsMusicSynth { get; set; }

        public bool IsMovieAction { get; set; }

        public bool IsMovieAnimation { get; set; }

        public bool IsMovieAdventure { get; set; }

        public bool IsMovieDocumentary { get; set; }

        public bool IsMovieDrama { get; set; }

        public bool IsMovieErotic { get; set; }

        public bool IsMovieFantasy { get; set; }

        public bool IsMovieHistoric { get; set; }

        public bool IsMovieIndependent { get; set; }

        public bool IsMovieComedy { get; set; }

        public bool IsMovieShort { get; set; }

        public bool IsMovieWar { get; set; }

        public bool IsMovieCrime { get; set; }

        public bool IsMovieManga { get; set; }

        public bool IsMovieMusic { get; set; }

        public bool IsMovieRomantic { get; set; }

        public bool IsMovieThriller { get; set; }

        public bool IsMovieSciFi { get; set; }

        public bool IsMovieHorror { get; set; }

        public bool IsMovieCartoon { get; set; }

        public bool IsMovieWestern { get; set; }

        public bool IsHobbyNewAge { get; set; }

        public bool IsHobbyCars { get; set; }

        public bool IsHobbyDance { get; set; }

        public bool IsHobbyAnimals { get; set; }

        public bool IsHobbyMovies { get; set; }

        public bool IsHobbyHunting { get; set; }

        public bool IsHobbyInterior { get; set; }

        public bool IsHobbyPhoto { get; set; }

        public bool IsHobbyOutdoors { get; set; }

        public bool IsHobbyArt { get; set; }

        public bool IsHobbyGame { get; set; }

        public bool IsHobbyCooking { get; set; }

        public bool IsHobbyReading { get; set; }

        public bool IsHobbyHistory { get; set; }

        public bool IsHobbyPainting { get; set; }

        public bool IsHobbyMuseum { get; set; }

        public bool IsHobbyMusic { get; set; }

        public bool IsHobbyPolitics { get; set; }

        public bool IsHobbyTravel { get; set; }

        public bool IsHobbyRolePlaying { get; set; }

        public bool IsHobbyShopping { get; set; }

        public bool IsHobbySinging { get; set; }

        public bool IsHobbyWriting { get; set; }

        public bool IsHobbyGaming { get; set; }

        public bool IsHobbyWorkout { get; set; }

        public bool IsHobbyDrama { get; set; }

        public bool IsHobbyDrawing { get; set; }

        public bool IsHobbyGardening { get; set; }

        public bool IsHobbyTv { get; set; }

        public bool IsHobbyCircles { get; set; }

        public bool IsHobbyHiking { get; set; }

        public bool IsHobbyWine { get; set; }

        public bool IsWeekendActive { get; set; }

        public bool IsWeekendChill { get; set; }

        public bool IsWeekendRandom { get; set; }

        public bool IsWeekendPlanned { get; set; }

        public bool IsSportDance { get; set; }

        public bool IsSportBalls { get; set; }

        public bool IsSportMartial { get; set; }

        public bool IsSportFitness { get; set; }

        public bool IsSportAdventure { get; set; }

        public bool IsSportGolf { get; set; }

        public bool IsSportRunning { get; set; }

        public bool IsSportRiding { get; set; }

        public bool IsSportSailing { get; set; }

        public bool IsSportSkating { get; set; }

        public bool IsSportSurfing { get; set; }

        public bool IsSportYoga { get; set; }

        public bool IsSportRaquet { get; set; }

        public bool IsExcerNot { get; set; }

        public bool IsExcerSome { get; set; }

        public bool IsExcerRegular { get; set; }

        public bool IsExcerAlways { get; set; }

        public bool IsDateBreakfast { get; set; }

        public bool IsDateLunch { get; set; }

        public bool IsDateDinner { get; set; }

        public bool IsDateMovie { get; set; }

        public bool IsDateBarSnack { get; set; }

        public bool IsDateDrinks { get; set; }

        public bool IsDatePicnic { get; set; }

        public bool IsDateDinnerHome { get; set; }

        public bool IsDateFika { get; set; }

        public bool IsLooksImportant { get; set; }

        public bool IsLooksLessImportant { get; set; }

        public bool IsRelationTogether { get; set; }

        public bool IsRelationOwnTime { get; set; }

        public bool IsRelationMarriage { get; set; }

        public bool IsRelationLiving { get; set; }

        public bool IsRelationNotLiving { get; set; }

        public int DatePackageId { get { return 1; } }

        public int ReferrerId { get; set; }

        public string Note1 { get; set; }

        public string Note2 { get; set; }

        public int OriginId { get { return DataCache.Origins.JamDating; } }

        public bool IsMale { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public bool IsSearchigForMale { get { return WhatSearchingForWhatId == 3 || WhatSearchingForWhatId == 4; } }
    }
}
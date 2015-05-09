using System.Linq;

using Dapper;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class SurveyDataService : ISurveyDataService
    {
        protected readonly IDatabaseConfigurationService Config;
        private readonly IUserProfile _userProfile;
        private readonly IAccountService _accountService;

        public SurveyDataService(IAccountService accountService, IUserProfile userProfile, IDatabaseConfigurationService configurationService)
        {
            _accountService = accountService;
            _userProfile = userProfile;
            Config = configurationService;
        }

        public Survey GetCurrentUserSurvey()
        {
            string email = _accountService.GetCurrentUserEmail();

            Survey survey = GetSurvey(email);

            if (survey == null)
            {
                survey = new Survey();
                survey.Email = email;
                survey.Name = _accountService.GetCurrentUserCommentRealName();
            }

            return survey;
        }

        public int GetCurrentUserSurveyId()
        {
            string email = _accountService.GetCurrentUserEmail();

            int surveyId = GetSurveyId(email);
            return surveyId;
        }

        public WantedSurvey GetCurrentUserWantedSurvey()
        {
            string email = _accountService.GetCurrentUserEmail();

            WantedSurvey wantedSurvey = GetWantedSurvey(email);
            return wantedSurvey;
        }

        public WantedSurvey GetWantedSurvey(int surveyId)
        {
            const string sqlGetWantedSurvey = "SELECT WantedSurveys.* FROM WantedSurveys "
                                              + "INNER JOIN Surveys ON WantedSurveys.WantedSurveyId = Surveys.WantedSurveyId WHERE Surveys.SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                if (surveyId > 0)
                {
                    var wantedSurvey = cn.Query<WantedSurvey>(sqlGetWantedSurvey, new { surveyId }).FirstOrDefault();
                    return wantedSurvey;
                }
            }

            return null;
        }

        public Survey GetSurvey(int surveyId)
        {
            const string sqlGetSurvey = "SELECT Surveys.*, GeoInfo.Coordinates.Lat AS Lat, GeoInfo.Coordinates.Long AS Long FROM Surveys LEFT OUTER JOIN GeoInfo ON Surveys.SurveyId = GeoInfo.SurveyId WHERE Surveys.SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                var survey = cn.Query<Survey>(sqlGetSurvey, new { surveyId }).FirstOrDefault();
                return survey;
            }
        }

        public bool SavePage1(Survey partSurvey)
        {
            const string sqlSurveyExists = "SELECT surveyId FROM Surveys WHERE Email = @Email";
            const string sqlInsertSurvey = "INSERT INTO Surveys (Name, City, Email, KidsCountId, KidsWantedCountId, WantedKidsWantedCountId, MotherLanguage, Height, Weight, Birth, DatePackageId, ReferrerId, OriginId) "
                                                     + "VALUES (@Name, @City, @Email, @KidsCountId, @KidsWantedCountId, 1, '', 1, 2, @Birth, 1, 1, @OriginId);  SELECT CAST(SCOPE_IDENTITY() AS int)";
            const string sqlUpdateSurveyPage1 = "UPDATE Surveys SET Name = @Name, "
                                                + "Street = @Street, "
                                                + "PostalCode = @PostalCode, "
                                                + "City = @City, "
                                                + "Country = @Country, " /* not email */
                                                + "Phone = @Phone, "

                                                + "Birth = @Birth, "
                //// + "IsMale = @IsMale, "
                //// + "IsSearchingForMale = @IsSearchingForMale, "
                                                + "Height = @Height, "
                                                + "Weight = @Weight, "

                                                + "Note1 = @Note1, "

                                                + "MotherLanguage = @MotherLanguage, "
                                                + "IsEthnicNorthEuropean = @IsEthnicNorthEuropean, "
                                                + "IsEthnicLatin = @IsEthnicLatin, "
                                                + "IsEthnicMulat = @IsEthnicMulat, "
                                                + "IsEthnicDark = @IsEthnicDark, "
                                                + "IsEthnicAsian = @IsEthnicAsian, "
                                                + "IsEthnicCaucasic = @IsEthnicCaucasic, "
                                                + "IsEthnicMiddleEastern = @IsEthnicMiddleEastern, "
                                                + "IsSingle = @IsSingle, "
                                                + "IsSeparated = @IsSeparated, "
                                                + "IsDivorced = @IsDivorced, "
                                                + "IsWidowed = @IsWidowed, "

                                                + "LivingAloneWithVisitors = @LivingAloneWithVisitors, "
                                                + "LivingAlone = @LivingAlone, "
                                                + "LivingWithPets = @LivingWithPets, "
                                                + "LivingWithKids = @LivingWithKids, "
                                                + "LivingWithParents = @LivingWithParents, "
                                                + "LivingWithFriends = @LivingWithFriends, "
                                                + "LivingWithPartTimeKids = @LivingWithPartTimeKids, "

                                                + "KidsCountId = @KidsCountId, "
                                                + "WhatSearchingForWhatId = @WhatSearchingForWhatId, "

                                                + "IsBodySlim = @IsBodySlim, "
                                                + "IsBodyAverage = @IsBodyAverage, "
                                                + "IsBodyAthletic = @IsBodyAthletic, "
                                                + "IsBodyHeavy = @IsBodyHeavy, "
                                                + "IsBodyChubby = @IsBodyChubby, "

                                                + "ChangeDate = getdate() WHERE SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                int surveyId = cn.Query<int>(sqlSurveyExists, partSurvey).FirstOrDefault();
                if (surveyId == 0)
                {
                    surveyId = cn.Query<int>(sqlInsertSurvey, partSurvey).Single();

                    partSurvey.SurveyId = surveyId;
                    cn.Execute(sqlUpdateSurveyPage1, partSurvey);
                }
                else
                {
                    partSurvey.SurveyId = surveyId;
                    cn.Execute(sqlUpdateSurveyPage1, partSurvey);
                }
            }

            return true;
        }

        public bool SavePage2(Survey partSurvey)
        {
            const string sqlSurveyExists = "SELECT surveyId FROM Surveys WHERE SurveyId = @SurveyId";
            const string sqlUpdateSurveyPage2 = "UPDATE Surveys SET IsHairBlond = @IsHairBlond, "
                                                + "IsHairRed = @IsHairRed, "
                                                + "IsHairBrown = @IsHairBrown, "
                                                + "IsHairGrey = @IsHairGrey, "
                                                + "IsHairBlack = @IsHairBlack, "
                                                + "IsHairBald = @IsHairBald, "

                                                + "IsHairLengthShaved = @IsHairLengthShaved, "
                                                + "IsHairLengthVeryShort = @IsHairLengthVeryShort, "
                                                + "IsHairLengthShort = @IsHairLengthShort, "
                                                + "IsHairLengthShoulder = @IsHairLengthShoulder, "
                                                + "IsHairLengthLong = @IsHairLengthLong, "
                                                + "IsHairLengthVeryLong = @IsHairLengthVeryLong, "

                                                + "IsEyesBlue = @IsEyesBlue, "
                                                + "IsEyesBrown = @IsEyesBrown, "
                                                + "IsEyesGrey = @IsEyesGrey, "
                                                + "IsEyesGreen = @IsEyesGreen, "
                                                + "IsEyesMix = @IsEyesMix, "

                                                + "IsProfNot = @IsProfNot, "
                                                + "IsProfAdmin = @IsProfAdmin, "
                                                + "IsProfArtist = @IsProfArtist, "
                                                + "IsProfConstruction = @IsProfConstruction, "
                                                + "IsProfHr = @IsProfHr, "
                                                + "IsProfIt = @IsProfIt, "
                                                + "IsProfDetail = @IsProfDetail, "
                                                + "IsProfSelf = @IsProfSelf, "
                                                + "IsProfFinance = @IsProfFinance, "
                                                + "IsProfSales = @IsProfSales, "
                                                + "IsProfHealth = @IsProfHealth, "
                                                + "IsProfHotel = @IsProfHotel, "
                                                + "IsProfLaw = @IsProfLaw, "
                                                + "IsProfAdvertising = @IsProfAdvertising, "
                                                + "IsProfState = @IsProfState, "
                                                + "IsProfEducation = @IsProfEducation, "
                                                + "IsProfRetired = @IsProfRetired, "
                                                + "IsProfStudent = @IsProfStudent, "

                                                + "IsEduBasic = @IsEduBasic, "
                                                + "IsEduCollage = @IsEduCollage, "
                                                + "IsEduUniversity = @IsEduUniversity, "
                                                + "IsEduKand = @IsEduKand, "
                                                + "IsEduMag = @IsEduMag, "
                                                + "IsEduDoc = @IsEduDoc, "
                                                + "IsEduLife = @IsEduLife, "

                                                + "IsPersonAdventurous = @IsPersonAdventurous, "
                                                + "IsPersonCarefree = @IsPersonCarefree, "
                                                + "IsPersonShy = @IsPersonShy, "
                                                + "IsPersonDominant = @IsPersonDominant, "
                                                + "IsPersonLoner = @IsPersonLoner, "
                                                + "IsPersonStubborn = @IsPersonStubborn, "
                                                + "IsPersonGenerous = @IsPersonGenerous, "
                                                + "IsPersonHappy = @IsPersonHappy, "
                                                + "IsPersonHomorous = @IsPersonHomorous, "
                                                + "IsPersonSensitive = @IsPersonSensitive, "
                                                + "IsPersonAmbitious = @IsPersonAmbitious, "
                                                + "IsPersonAnalytic = @IsPersonAnalytic, "
                                                + "IsPersonTemper = @IsPersonTemper, "
                                                + "IsPersonLivly = @IsPersonLivly, "
                                                + "IsPersonCalm = @IsPersonCalm, "
                                                + "IsPersonRestless = @IsPersonRestless, "
                                                + "IsPersonConsidering = @IsPersonConsidering, "
                                                + "IsPersonReserved = @IsPersonReserved, "
                                                + "IsPersonSocial = @IsPersonSocial, "
                                                + "IsPersonSpantanious = @IsPersonSpantanious, "
                                                + "IsPersonCaring = @IsPersonCaring, "
                                                + "IsPersonInspiring = @IsPersonInspiring, "
                                                + "IsPersonSafe = @IsPersonSafe, "
                                                + "IsPersonStructured = @IsPersonStructured, "

                                                + "ChangeDate = getdate() WHERE SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                int surveyId = cn.Query<int>(sqlSurveyExists, partSurvey).FirstOrDefault();
                if (surveyId == 0)
                {
                    return false;
                }
                else
                {
                    cn.Execute(sqlUpdateSurveyPage2, partSurvey);
                }
            }

            return true;
        }

        public bool SavePage3(Survey partSurvey)
        {
            const string sqlSurveyExists = "SELECT surveyId FROM Surveys WHERE SurveyId = @SurveyId";
            const string sqlUpdateSurveyPage3 = "UPDATE Surveys SET IsExcerNot = @IsExcerNot, "
                                                + "IsExcerSome = @IsExcerSome, "
                                                + "IsExcerRegular = @IsExcerRegular, "
                                                + "IsExcerAlways = @IsExcerAlways, "

                                                + "IsRelAdventist = @IsRelAdventist, "
                                                + "IsRelAgnostic = @IsRelAgnostic, "
                                                + "IsRelSpiritual = @IsRelSpiritual, "
                                                + "IsRelAnglican = @IsRelAnglican, "
                                                + "IsRelAtheist = @IsRelAtheist, "
                                                + "IsRelBaptist = @IsRelBaptist, "
                                                + "IsRelHindu = @IsRelHindu, "
                                                + "IsRelBuddhist = @IsRelBuddhist, "
                                                + "IsRelJewish = @IsRelJewish, "
                                                + "IsRelCatholic = @IsRelCatholic, "
                                                + "IsRelChistian = @IsRelChistian, "
                                                + "IsRelMethodist = @IsRelMethodist, "
                                                + "IsRelMormon = @IsRelMormon, "
                                                + "IsRelMuslim = @IsRelMuslim, "
                                                + "IsRelOrtodox = @IsRelOrtodox, "
                                                + "IsRelPingst = @IsRelPingst, "
                                                + "IsRelProtestant = @IsRelProtestant, "
                                                + "IsRelTao = @IsRelTao, "
                                                + "IsRelOther = @IsRelOther, "

                                                + "IsRelPracticing = @IsRelPracticing, "
                                                + "IsRelSomewhat = @IsRelSomewhat, "
                                                + "IsRelNotPracticing = @IsRelNotPracticing, "

                                                + "IsIncomeLess = @IsIncomeLess, "
                                                + "IsIncome1 = @IsIncome1, "
                                                + "IsIncome2 = @IsIncome2, "
                                                + "IsIncome3 = @IsIncome3, "
                                                + "IsIncome4 = @IsIncome4, "
                                                + "IsIncome5 = @IsIncome5, "
                                                + "IsIncomeMore = @IsIncomeMore, "

                                                + "IsTobacQuitting=@IsTobacQuitting, "
                                                + "IsTobacCigar=@IsTobacCigar, "
                                                + "IsTobacRegularly=@IsTobacRegularly, "
                                                + "IsTobacParty=@IsTobacParty, "
                                                + "IsTobacSnus=@IsTobacSnus, "
                                                + "IsTobacNot=@IsTobacNot, "

                                                + "IsAlcNot=@IsAlcNot, "
                                                + "IsAlcLittle=@IsAlcLittle, "
                                                + "IsAlcSocially=@IsAlcSocially, "
                                                + "IsAlcMuch=@IsAlcMuch, "

                                                + "IsPleWiArms=@IsPleWiArms, "
                                                + "IsPleWiChest=@IsPleWiChest, "
                                                + "IsPleWiLegs=@IsPleWiLegs, "
                                                + "IsPleWiFeet=@IsPleWiFeet, "
                                                + "IsPleWiNeck=@IsPleWiNeck, "
                                                + "IsPleWiHands=@IsPleWiHands, "
                                                + "IsPleWiHair=@IsPleWiHair, "
                                                + "IsPleWiSmile=@IsPleWiSmile, "
                                                + "IsPleWiLips=@IsPleWiLips, "
                                                + "IsPleWiMuscles=@IsPleWiMuscles, "
                                                + "IsPleWiBelly=@IsPleWiBelly, "
                                                + "IsPleWiEars=@IsPleWiEars, "
                                                + "IsPleWiEyes=@IsPleWiEyes, "
                                                + "IsPleWiButt=@IsPleWiButt, "
                                                + "IsPleWiCalf=@IsPleWiCalf, "

                                                + "ChangeDate = getdate() WHERE SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                int surveyId = cn.Query<int>(sqlSurveyExists, partSurvey).FirstOrDefault();
                if (surveyId == 0)
                {
                    return false;
                }
                else
                {
                    cn.Execute(sqlUpdateSurveyPage3, partSurvey);
                }
            }

            return true;
        }

        public bool SavePage4(Survey partSurvey)
        {
            const string sqlSurveyExists = "SELECT surveyId FROM Surveys WHERE SurveyId = @SurveyId";
            const string sqlUpdateSurveyPage4 = "UPDATE Surveys SET IsDressBoheme = @IsDressBoheme, "
                                                + "IsDressBusiness = @IsDressBusiness, "
                                                + "IsDressEthnic = @IsDressEthnic, "
                                                + "IsDressClassic = @IsDressClassic, "
                                                + "IsDressFashion = @IsDressFashion, "
                                                + "IsDressRock = @IsDressRock, "
                                                + "IsDressSophisticated = @IsDressSophisticated, "
                                                + "IsDressSport = @IsDressSport, "
                                                + "IsDressTrend = @IsDressTrend, "

                                                + "IsBodyArtNever = @IsBodyArtNever, "
                                                + "IsBodyArtVisible = @IsBodyArtVisible, "
                                                + "IsBodyArtHidden = @IsBodyArtHidden, "
                                                + "IsBodyArtPiercing = @IsBodyArtPiercing, "
                                                + "IsBodyArtAsk = @IsBodyArtAsk, "

                                                + "IsRomanticVery = @IsRomanticVery, "
                                                + "IsRomantic = @IsRomantic, "
                                                + "IsRomanticLittle = @IsRomanticLittle, "
                                                + "IsRomanticNot = @IsRomanticNot, "

                                                + "IsMarriageHoly = @IsMarriageHoly, "
                                                + "IsMarriageVeryImportant = @IsMarriageVeryImportant, "
                                                + "IsMarriageImportant = @IsMarriageImportant, "
                                                + "IsMarriageLessImportant = @IsMarriageLessImportant, "
                                                + "IsMarriageUnImportant = @IsMarriageUnImportant, "
                                                + "IsMarriageNotAgain = @IsMarriageNotAgain, "

                                                + "IsPoliticVeryConservative = @IsPoliticVeryConservative, "
                                                + "IsPoliticConservative = @IsPoliticConservative, "
                                                + "IsPoliticMiddle = @IsPoliticMiddle, "
                                                + "IsPoliticLiberal = @IsPoliticLiberal, "
                                                + "IsPoliticVeryLiberal = @IsPoliticVeryLiberal, "
                                                + "IsPoliticNonConformist= @IsPoliticNonConformist, "
                                                + "IsPoliticOther = @IsPoliticOther, "

                                                + "IsMusicNewAge=@IsMusicNewAge, "
                                                + "IsMusicBlues=@IsMusicBlues, "
                                                + "IsMusicCountry=@IsMusicCountry, "
                                                + "IsMusicHits=@IsMusicHits, "
                                                + "IsMusicDisco=@IsMusicDisco, "
                                                + "IsMusicElectronica=@IsMusicElectronica, "
                                                + "IsMusicEthnic=@IsMusicEthnic, "
                                                + "IsMusicFilm=@IsMusicFilm, "
                                                + "IsMusicFolk=@IsMusicFolk, "
                                                + "IsMusicFunk=@IsMusicFunk, "
                                                + "IsMusicGospel=@IsMusicGospel, "
                                                + "IsMusicMetal=@IsMusicMetal, "
                                                + "IsMusicHipHop=@IsMusicHipHop, "
                                                + "IsMusicJazz=@IsMusicJazz, "
                                                + "IsMusicClassic=@IsMusicClassic, "
                                                + "IsMusicLatin=@IsMusicLatin, "
                                                + "IsMusicMusical=@IsMusicMusical, "
                                                + "IsMusicOpera=@IsMusicOpera, "
                                                + "IsMusicPop=@IsMusicPop, "
                                                + "IsMusicRnB=@IsMusicRnB, "
                                                + "IsMusicRock=@IsMusicRock, "
                                                + "IsMusicReggae=@IsMusicReggae, "
                                                + "IsMusicSoul=@IsMusicSoul, "
                                                + "IsMusicSynth=@IsMusicSynth, "

                                                + "IsMovieAction=@IsMovieAction, "
                                                + "IsMovieAnimation=@IsMovieAnimation, "
                                                + "IsMovieAdventure=@IsMovieAdventure, "
                                                + "IsMovieDocumentary=@IsMovieDocumentary, "
                                                + "IsMovieDrama=@IsMovieDrama, "
                                                + "IsMovieErotic=@IsMovieErotic, "
                                                + "IsMovieFantasy=@IsMovieFantasy, "
                                                + "IsMovieHistoric=@IsMovieHistoric, "
                                                + "IsMovieIndependent=@IsMovieIndependent, "
                                                + "IsMovieComedy=@IsMovieComedy, "
                                                + "IsMovieShort=@IsMovieShort, "
                                                + "IsMovieWar=@IsMovieWar, "
                                                + "IsMovieCrime=@IsMovieCrime, "
                                                + "IsMovieManga=@IsMovieManga, "
                                                + "IsMovieMusic=@IsMovieMusic, "
                                                + "IsMovieRomantic=@IsMovieRomantic, "
                                                + "IsMovieThriller=@IsMovieThriller, "
                                                + "IsMovieSciFi=@IsMovieSciFi, "
                                                + "IsMovieHorror=@IsMovieHorror, "
                                                + "IsMovieCartoon=@IsMovieCartoon, "
                                                + "IsMovieWestern=@IsMovieWestern, "

                                                + "ChangeDate = getdate() WHERE SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                int surveyId = cn.Query<int>(sqlSurveyExists, partSurvey).FirstOrDefault();
                if (surveyId == 0)
                {
                    return false;
                }
                else
                {
                    cn.Execute(sqlUpdateSurveyPage4, partSurvey);
                }
            }

            return true;
        }

        public bool SavePage5(Survey partSurvey)
        {
            const string sqlSurveyExists = "SELECT surveyId FROM Surveys WHERE SurveyId = @SurveyId";
            const string sqlUpdateSurveyPage5 = "UPDATE Surveys SET IsHobbyNewAge= @IsHobbyNewAge, "
                                                + "IsHobbyCars = @IsHobbyCars, "
                                                + "IsHobbyDance = @IsHobbyDance, "
                                                + "IsHobbyAnimals = @IsHobbyAnimals, "
                                                + "IsHobbyMovies = @IsHobbyMovies, "
                                                + "IsHobbyHunting = @IsHobbyHunting, "
                                                + "IsHobbyInterior = @IsHobbyInterior, "
                                                + "IsHobbyPhoto = @IsHobbyPhoto, "
                                                + "IsHobbyOutdoors = @IsHobbyOutdoors, "
                                                + "IsHobbyArt = @IsHobbyArt, "
                                                + "IsHobbyGame = @IsHobbyGame, "
                                                + "IsHobbyCooking = @IsHobbyCooking, "
                                                + "IsHobbyReading = @IsHobbyReading, "
                                                + "IsHobbyHistory = @IsHobbyHistory, "
                                                + "IsHobbyPainting = @IsHobbyPainting, "
                                                + "IsHobbyMuseum = @IsHobbyMuseum, "
                                                + "IsHobbyMusic = @IsHobbyMusic, "
                                                + "IsHobbyPolitics = @IsHobbyPolitics, "
                                                + "IsHobbyTravel = @IsHobbyTravel, "
                                                + "IsHobbyRolePlaying = @IsHobbyRolePlaying, "
                                                + "IsHobbyShopping = @IsHobbyShopping, "
                                                + "IsHobbyWriting = @IsHobbyWriting, "
                                                + "IsHobbyGaming = @IsHobbyGaming, "
                                                + "IsHobbyWorkout = @IsHobbyWorkout, "
                                                + "IsHobbyDrama = @IsHobbyDrama, "
                                                + "IsHobbyDrawing = @IsHobbyDrawing, "
                                                + "IsHobbyGardening = @IsHobbyGardening, "
                                                + "IsHobbyTv = @IsHobbyTv, "
                                                + "IsHobbyCircles = @IsHobbyCircles, "
                                                + "IsHobbyHiking = @IsHobbyHiking, "
                                                + "IsHobbyWine = @IsHobbyWine, "

                                                + "IsWeekendActive=@IsWeekendActive, "
                                                + "IsWeekendChill=@IsWeekendChill, "
                                                + "IsWeekendRandom=@IsWeekendRandom, "
                                                + "IsWeekendPlanned=@IsWeekendPlanned, "

                                                + "IsSportDance=@IsSportDance, "
                                                + "IsSportBalls=@IsSportBalls, "
                                                + "IsSportMartial=@IsSportMartial, "
                                                + "IsSportFitness=@IsSportFitness, "
                                                + "IsSportAdventure=@IsSportAdventure, "
                                                + "IsSportGolf=@IsSportGolf, "
                                                + "IsSportRunning=@IsSportRunning, "
                                                + "IsSportRiding=@IsSportRiding, "
                                                + "IsSportSailing=@IsSportSailing, "
                                                + "IsSportSkating=@IsSportSkating, "
                                                + "IsSportSurfing=@IsSportSurfing, "
                                                + "IsSportYoga=@IsSportYoga, "
                                                + "IsSportRaquet=@IsSportRaquet, "

                                                + "IsDateBreakfast=@IsDateBreakfast, "
                                                + "IsDateLunch=@IsDateLunch, "
                                                + "IsDateDinner=@IsDateDinner, "
                                                + "IsDateMovie=@IsDateMovie, "
                                                + "IsDateBarSnack=@IsDateBarSnack, "
                                                + "IsDateDrinks=@IsDateDrinks, "
                                                + "IsDatePicnic=@IsDatePicnic, "
                                                + "IsDateDinnerHome=@IsDateDinnerHome, "
                                                + "IsDateFika=@IsDateFika, "

                                                + "IsLooksImportant=@IsLooksImportant, "
                                                + "IsLooksLessImportant=@IsLooksLessImportant, "

                                                + "IsRelationTogether=@IsRelationTogether, "
                                                + "IsRelationOwnTime=@IsRelationOwnTime, "
                                                + "IsRelationMarriage=@IsRelationMarriage, "
                                                + "IsRelationLiving=@IsRelationLiving, "
                                                + "IsRelationNotLiving=@IsRelationNotLiving, "

                                                + "ChangeDate = getdate() WHERE SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                int surveyId = cn.Query<int>(sqlSurveyExists, partSurvey).FirstOrDefault();
                if (surveyId == 0)
                {
                    return false;
                }
                else
                {
                    cn.Execute(sqlUpdateSurveyPage5, partSurvey);
                }
            }

            return true;
        }

        public bool SavePage6(Survey partSurvey, WantedSurvey wantedSurvey)
        {
            const string sqlSurveyExists = "SELECT surveyId FROM Surveys WHERE SurveyId = @SurveyId";
            const string sqlUpdateSurveyPage6 = "UPDATE Surveys SET  "

                                                + "WantedKidsWantedCountId = @WantedKidsWantedCountId, "

                                                + "ReferrerId = @ReferrerId, "
                                                + "DatePackageId = @DatePackageId, "
                                                + "ChangeDate = getdate() WHERE SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                int surveyId = cn.Query<int>(sqlSurveyExists, partSurvey).FirstOrDefault();
                if (surveyId == 0)
                {
                    return false;
                }
                else
                {
                    cn.Execute(sqlUpdateSurveyPage6, partSurvey);

                    SavePage6Wanted(partSurvey, wantedSurvey);
                }
            }

            return true;
        }

        public int GetSurveyId(string email)
        {
            int surveyId = _userProfile.GetSurveyId(email);
            if (surveyId > 0)
            {
                return surveyId;
            }

            const string sqlSurveyExists = "SELECT surveyId FROM Surveys WHERE Email = @Email";

            using (var cn = Config.CreateConnection())
            {
                surveyId = cn.Query<int>(sqlSurveyExists, new { Email = email }).FirstOrDefault();
                _userProfile.SetSurveyId(email, surveyId);
                return surveyId;
            }
        }

        public bool HideSurvey(int surveyId)
        {
            return HideUnhideSurvey(surveyId, true);
        }

        public bool UnhideSurvey(int surveyId)
        {
            return HideUnhideSurvey(surveyId, false);
        }

        private bool HideUnhideSurvey(int surveyId, bool doHide)
        {
            const string sqlHideUnhideSurvey = "UPDATE Surveys SET IsDisabled = @IsDisabled WHERE SurveyId = @SurveyId";
            using (var cn = Config.CreateConnection())
            {
                int nbrOfRows = cn.Execute(sqlHideUnhideSurvey, new { IsDisabled = doHide, SurveyId = surveyId });
                return nbrOfRows == 1;
            }
        }

        private void SavePage6Wanted(Survey survey, WantedSurvey wantedSurvey)
        {
            const string sqlWantedSurveyExists = "SELECT Surveys.WantedSurveyId FROM WantedSurveys "
                                                 + "INNER JOIN Surveys ON WantedSurveys.WantedSurveyId = Surveys.WantedSurveyId WHERE Surveys.SurveyId = @SurveyId";
            const string sqlInsertWantedSurvey = "INSERT INTO WantedSurveys (AgeMin, AgeMax) "
                                                + "VALUES (0, 0); SELECT CAST(SCOPE_IDENTITY() AS int)";
            const string sqlUpdateSurveyPage6Wanted = "UPDATE WantedSurveys SET Is150 = @Is150, "
                                                + "Is160 = @Is160, "
                                                + "Is170 = @Is170, "
                                                + "Is180 = @Is180, "
                                                + "Is190 = @Is190, "

                                                + "AgeMin = @AgeMin, "
                                                + "AgeMax = @AgeMax, "

                                                + "IsBodySlim = @IsBodySlim, "
                                                + "IsBodyAverage = @IsBodyAverage, "
                                                + "IsBodyAthletic = @IsBodyAthletic, "
                                                + "IsBodyChubby = @IsBodyChubby, "
                                                + "IsBodyHeavy = @IsBodyHeavy, "

                                                + "IsHobbySport = @IsHobbySport, "
                                                + "IsHobbyMusic = @IsHobbyMusic, "
                                                + "IsHobbyNature = @IsHobbyNature, "
                                                + "IsHobbyCulture = @IsHobbyCulture, "
                                                + "IsHobbyFriends = @IsHobbyFriends, "
                                                + "IsHobbyTech = @IsHobbyTech "

                                                + "WHERE WantedSurveyId = @WantedSurveyId";
            const string sqlUpdateWantedSurveyIdOnSurvey = "UPDATE Surveys SET WantedSurveyId = @WantedSurveyId WHERE SurveyId = @surveyId";

            using (var cn = Config.CreateConnection())
            {
                int wantedSurveyId = cn.Query<int>(sqlWantedSurveyExists, wantedSurvey).FirstOrDefault();
                if (wantedSurveyId == 0)
                {
                    wantedSurveyId = cn.Query<int>(sqlInsertWantedSurvey, wantedSurvey).Single();
                    survey.WantedSurveyId = wantedSurveyId;
                    cn.Execute(sqlUpdateWantedSurveyIdOnSurvey, survey);

                    wantedSurvey.WantedSurveyId = wantedSurveyId;
                    cn.Execute(sqlUpdateSurveyPage6Wanted, wantedSurvey);
                }
                else
                {
                    wantedSurvey.WantedSurveyId = wantedSurveyId;
                    cn.Execute(sqlUpdateSurveyPage6Wanted, wantedSurvey);
                }
            }
        }

        private Survey GetSurvey(string email)
        {
            const string sqlGetSurvey = "SELECT * FROM Surveys WHERE SurveyId = @SurveyId";

            int surveyId = GetSurveyId(email);
            if (surveyId > 0)
            {
                using (var cn = Config.CreateConnection())
                {
                    var survey = cn.Query<Survey>(sqlGetSurvey, new { surveyId }).FirstOrDefault();
                    return survey;
                }
            }

            return null;
        }

        private WantedSurvey GetWantedSurvey(string email)
        {
            const string sqlWantedSurveyExists = "SELECT Surveys.WantedSurveyId FROM WantedSurveys "
                                                 + "INNER JOIN Surveys ON WantedSurveys.WantedSurveyId = Surveys.WantedSurveyId WHERE Surveys.Email = @Email";
            const string sqlGetWantedSurvey = "SELECT * FROM WantedSurveys WHERE WantedSurveyId = @WantedSurveyId";

            using (var cn = Config.CreateConnection())
            {
                int wantedSurveyId = cn.Query<int>(sqlWantedSurveyExists, new { email }).FirstOrDefault();
                if (wantedSurveyId > 0)
                {
                    var wantedSurvey = cn.Query<WantedSurvey>(sqlGetWantedSurvey, new { wantedSurveyId }).FirstOrDefault();
                    return wantedSurvey;
                }
            }

            return null;
        }
    }
}
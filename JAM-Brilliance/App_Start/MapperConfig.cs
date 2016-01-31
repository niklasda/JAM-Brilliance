using System.Web.Security;
using AutoMapper;

using JAM.Core.Models;
using JAM.Brilliance.Models.ViewModels;

namespace JAM.Brilliance
{
    public static class MapperConfig
    {
        public static IMapper CreateAutoMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<SparkleProfile>();
            });

            return config.CreateMapper();

            /* Mapper.CreateMap<MembershipUser, Account>();
            Mapper.CreateMap<Account, AccountViewModel>();

            Mapper.CreateMap<SearchCriteria, SearchCriteriaViewModel>().ReverseMap();
            Mapper.CreateMap<SearchAdvCriteria, SearchAdvCriteriaViewModel>().ReverseMap();
            Mapper.CreateMap<SearchResult, SearchResultViewModel>().ReverseMap();

            Mapper.CreateMap<LogEntry, LogEntryViewModel>();
            Mapper.CreateMap<AbuseReport, AbuseReportViewModel>();
            Mapper.CreateMap<HistoryEntry, HistoryEntryViewModel>();
            Mapper.CreateMap<Favourite, FavouriteViewModel>();
            Mapper.CreateMap<SendMessage, SendMessageViewModel>().ReverseMap();
            Mapper.CreateMap<Conversation, ConversationViewModel>();

            Mapper.CreateMap<Survey, ShortSurveyViewModel>();
            Mapper.CreateMap<Survey, SurveyPage1ViewModel>().ReverseMap();
            Mapper.CreateMap<Survey, SurveyPage2ViewModel>().ReverseMap();
            Mapper.CreateMap<Survey, SurveyPage3ViewModel>().ReverseMap();
            Mapper.CreateMap<Survey, SurveyPage4ViewModel>().ReverseMap();
            Mapper.CreateMap<Survey, SurveyPage5ViewModel>().ReverseMap();
            Mapper.CreateMap<Survey, SurveyPage6ViewModel>().ReverseMap();

            Mapper.CreateMap<SurveySettings, SurveySettingsViewModel>();
            Mapper.CreateMap<WantedSurveyViewModel, WantedSurvey>().ReverseMap();
            Mapper.CreateMap<PictureViewModel, Picture>().ReverseMap();*/
        }

        private class SparkleProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<MembershipUser, Account>();
                CreateMap<Account, AccountViewModel>();

                CreateMap<SearchCriteria, SearchCriteriaViewModel>().ReverseMap();
                CreateMap<SearchAdvCriteria, SearchAdvCriteriaViewModel>().ReverseMap();
                CreateMap<SearchResult, SearchResultViewModel>().ReverseMap();

                CreateMap<LogEntry, LogEntryViewModel>();
                CreateMap<AbuseReport, AbuseReportViewModel>();
                CreateMap<HistoryEntry, HistoryEntryViewModel>();
                CreateMap<Favourite, FavouriteViewModel>();
                CreateMap<SendMessage, SendMessageViewModel>().ReverseMap();
                CreateMap<Conversation, ConversationViewModel>();

                CreateMap<Survey, ShortSurveyViewModel>();
                CreateMap<Survey, SurveyPage1ViewModel>().ReverseMap();
                CreateMap<Survey, SurveyPage2ViewModel>().ReverseMap();
                CreateMap<Survey, SurveyPage3ViewModel>().ReverseMap();
                CreateMap<Survey, SurveyPage4ViewModel>().ReverseMap();
                CreateMap<Survey, SurveyPage5ViewModel>().ReverseMap();
                CreateMap<Survey, SurveyPage6ViewModel>().ReverseMap();

                CreateMap<SurveySettings, SurveySettingsViewModel>();
                CreateMap<WantedSurveyViewModel, WantedSurvey>().ReverseMap();
                CreateMap<PictureViewModel, Picture>().ReverseMap();
            }
        }
    }
}
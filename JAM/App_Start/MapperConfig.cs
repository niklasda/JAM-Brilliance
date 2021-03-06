﻿using System.Web.Security;
using AutoMapper;

using JAM.Core.Models;
using JAM.Models.ViewModels;

namespace JAM
{
    public static class MapperConfig
    {
        public static IMapper CreateAutoMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<JamProfile>();
            });

            return config.CreateMapper();

            //Mapper.CreateMap<MembershipUser, Account>();
            //Mapper.CreateMap<Account, AccountViewModel>();

            //Mapper.CreateMap<SearchCriteria, SearchCriteriaViewModel>().ReverseMap();
            //Mapper.CreateMap<SearchAdvCriteria, SearchAdvCriteriaViewModel>().ReverseMap();
            //Mapper.CreateMap<SearchResult, SearchResultViewModel>().ReverseMap();

            //Mapper.CreateMap<LogEntry, LogEntryViewModel>();
            //Mapper.CreateMap<AbuseReport, AbuseReportViewModel>();
            //Mapper.CreateMap<HistoryEntry, HistoryEntryViewModel>();
            //Mapper.CreateMap<Favourite, FavouriteViewModel>();
            //Mapper.CreateMap<SendMessage, SendMessageViewModel>().ReverseMap();
            //Mapper.CreateMap<Conversation, ConversationViewModel>();

            //Mapper.CreateMap<Survey, ShortSurveyViewModel>();
            //Mapper.CreateMap<Survey, SurveyPage1ViewModel>().ReverseMap();
            //Mapper.CreateMap<Survey, SurveyPage2ViewModel>().ReverseMap();
            //Mapper.CreateMap<Survey, SurveyPage3ViewModel>().ReverseMap();
            //Mapper.CreateMap<Survey, SurveyPage4ViewModel>().ReverseMap();
            //Mapper.CreateMap<Survey, SurveyPage5ViewModel>().ReverseMap();
            //Mapper.CreateMap<Survey, SurveyPage6ViewModel>().ReverseMap();

            //Mapper.CreateMap<WantedSurveyViewModel, WantedSurvey>().ReverseMap();
            // Mapper.CreateMap<PictureViewModel, Picture>().ReverseMap();
        }

        private class JamProfile : Profile
        {
            protected override void Configure()
            {
                
                CreateMap<Survey, SurveyPage1ViewModel>().ReverseMap();
                CreateMap<Survey, SurveyPage2ViewModel>().ReverseMap();
           //     CreateMap<Survey, SurveyPage3ViewModel>().ReverseMap();
            //    CreateMap<Survey, SurveyPage4ViewModel>().ReverseMap();
            //    CreateMap<Survey, SurveyPage5ViewModel>().ReverseMap();
            //    CreateMap<Survey, SurveyPage6ViewModel>().ReverseMap();
                
            }
        }
    }
}
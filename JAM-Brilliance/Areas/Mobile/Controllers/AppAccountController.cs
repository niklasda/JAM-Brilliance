using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using JAM.Brilliance.Areas.Mobile.Attributes;
using JAM.Brilliance.Areas.Mobile.Models;
using JAM.Brilliance.Areas.Mobile.Models.Response;
using JAM.Brilliance.Models.ViewModels;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.App;
using JAM.Core.Logic;
using JAM.Core.Models;

namespace JAM.Brilliance.Areas.Mobile.Controllers
{
    public class AppAccountController : AppBaseController
    {
        private readonly IDiagnosticsService _diagnosticsService;
        private readonly IPostalCodeDataService _postalCodeDataService;
        private readonly ISurveyDataService _surveyDataService;

        public AppAccountController(IAccountService accountService, IAccountTokenDataService accountTokenDataService, IDiagnosticsService diagnosticsService, IPostalCodeDataService postalCodeDataService, ISurveyDataService surveyDataService)
            : base(accountService, accountTokenDataService)
        {
            _diagnosticsService = diagnosticsService;
            _postalCodeDataService = postalCodeDataService;
            _surveyDataService = surveyDataService;
        }

        [HttpPost]
        public JsonResult LoginSubmit(LoginModel model)
        {
            var ok = !string.IsNullOrWhiteSpace(model.UserName) && !string.IsNullOrWhiteSpace(model.Password);
            if (ok && Membership.ValidateUser(model.UserName, model.Password))
            {
                if (AccountService.IsUserMobileApp(model.UserName))
                {
                    // return the token
                    var utd = AccountTokenDataService.IssueToken(model.UserName);
                    var token = utd.Token;

                    return Json(new StatusModel { Success = true, Token = token, Message = "OK" });
                }
            }

            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            var response = new StatusModel { Success = false, Message = "NOK" };
            return Json(response);
        }

        [HttpPost]
        public JsonResult SignupSubmit(SignupModel model)
        {
            StatusModel response;
            var ok = !string.IsNullOrWhiteSpace(model.UserName) && !string.IsNullOrWhiteSpace(model.Password);

            bool databaseOk = _diagnosticsService.IsDatabaseOk();

            if (ok && databaseOk)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                var user = Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, /*isApproved:*/ true, out createStatus);

                if (createStatus == MembershipCreateStatus.Success && user != null)
                {
                    AccountService.MakeUserMobileApp(model.UserName);
                    CreateExtraUserSurvey(model);

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    response = new StatusModel { Success = true, Message = "OK" };
                    return Json(response);
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new StatusModel { Success = false, Message = createStatus.ToString() };
                    return Json(response);
                }
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            response = new StatusModel { Success = false, Message = "NOK" };
            return Json(response);
        }


        [HttpPost, ValidateToken]
        public JsonResult Logout()
        {
            AccountTokenDataService.RemoveToken(Token);
            return Json(new StatusModel { Success = true, Message = "OK" });
        }

        [HttpGet, Authorize(Roles = MemberRoles.Administrator)]
        public JsonResult CreateExtraUsers()
        {
            string[] names =
            {
                "Anna", "Bertil", "Carl", "David", "Erik", "Filip", "Gunnar", "Harald", "Ivan", "Jan",
                "Karin", "Louise", "Maria", "Nils", "Ove", "Per", "Rikard", "Stina", "Tor", "Urban", "Vera", "Walter", "Yngve", "Östen"
            };

            MembershipCreateStatus createStatus;

            for (int i = 0; i < 10; i++)
            {
                var name = names[i%(names.Length - 1)];
                var postal = GetRandomPostalCode();
                var email = string.Format("{0}@{1}.se", name, postal.City).ToLower();
                SignupModel model = new SignupModel()
                {
                    AmMan = i % 2 == 0,
                    AmWoman = i % 2 != 0,
                    WantMan = i % 2 != 0,
                    WantWoman = i % 2 == 0,
                    Country = "",
                    UserName = string.Format("{0}{1}", name, i),
                    Password = string.Format("{0}_{1}_{0}_{1}", name, i),
                    Email = email,
                    PostalCode = postal.PostalCode,
                };

                var user = Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, /*isApproved:*/ true, out createStatus);

                if (createStatus == MembershipCreateStatus.Success && user != null)
                {
                    AccountService.MakeUserMobileApp(model.UserName);
                   CreateExtraUserSurvey(model);
                }
            }

            return Json(new StatusModel { Success = true, Message = "OK" }, JsonRequestBehavior.AllowGet);
        }

        private void CreateExtraUserSurvey(SignupModel model)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            PostalCodeInfo postal = _postalCodeDataService.GetPostalCodeInfo(model.PostalCode);

            SurveyPage1ViewModel sp1vm = new SurveyPage1ViewModel();
            sp1vm.Name = string.Format("{0} {0}sson", model.UserName);
            sp1vm.Height = rnd.Next(150, 202);
            sp1vm.Weight = rnd.Next(60, 130);
            sp1vm.Birth = new DateTime(rnd.Next(1940, 1994), rnd.Next(1, 12), rnd.Next(1, 27));
            sp1vm.Email = model.Email;
            sp1vm.PostalCode = model.PostalCode;
            sp1vm.City = postal.City;
            sp1vm.Note1 = "Extra";
            sp1vm.WhatSearchingForWhatId = model.AmMan ? 2 : 4;

            Survey sp1 = Mapper.Map<Survey>(sp1vm);

            _surveyDataService.SavePage1(sp1);
        }

        private PostalCodeInfo GetRandomPostalCode()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            PostalCodeInfo pni;
            do
            {
                var pnr = rnd.Next(10000, 99999).ToString();
                pni = _postalCodeDataService.GetPostalCodeInfo(pnr);
            } while (pni == null);

            return pni;
        }
    }
}

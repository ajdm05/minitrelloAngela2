using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using AutoMapper;
using MiniTrello.Api.Models;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;
using RestSharp;


namespace MiniTrello.Api.Controllers.Helpers
{
    public class AccountHelpers
    {
        private static bool IsAValidEmail(String email)
        {
            const string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                return Regex.Replace(email, expresion, String.Empty).Length == 0;
            }
            return false;
        }

        private static bool IsASecurePassword(String password)
        {
            const string expresion =
                "^(.{0,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{4,})|(.{1,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{3,})|(.{2,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{2,})|(.{3,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{1,})|(.{4,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{0,})$";
            if (Regex.IsMatch(password, expresion))
            {
                return Regex.Replace(password, expresion, String.Empty).Length == 0;
            }
            return false;
        }

        public static bool IsAValidRegister(AccountRegisterModel model)
        {
            if (!IsAValidEmail(model.Email))
                throw new BadRequestException("The email is not valid");

            if (model.Password != model.ConfirmPassword)
                throw new BadRequestException("Password and Confirm Password are diferent");

            if (!IsASecurePassword(model.Password))
                throw new BadRequestException("The password has to have at least a cap and a min letter" +
                                              "a digit or a special character" +
                                              " and at least 6 characters");
            return true;
        }

        public static Sessions CreateNewSession(Account account)
        {
            var session = new Sessions
            {
                User = account,
                Creation =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                        DateTime.Now.Minute, DateTime.Now.Second),
                Duration =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                        DateTime.Now.Minute, DateTime.Now.Second).AddMinutes(500),
                Token = Guid.NewGuid().ToString(),
                Id = account.Id,
                IsArchived = false
            };
            return session;
        }

        public static Organization CreateOrganization(Account account)
        {
            var organization = new Organization
            {
                Title = "Welcome Organization",
                Administrador = account,
                Description = "This is your first Organization",
                IsArchived = false
            };
            organization.AddMember(account);
            organization.AddBoard(CreateBoard(account));
            account.AddOrganization(organization);
            return organization;
        }
        public static Organization CreateOrganizationDefault(Account account, string title, string desc)
        {
            var organization = new Organization
            {
                Title = title,
                Administrador = account,
                Description = desc,
                IsArchived = false
            };
            organization.AddMember(account);
            organization.AddBoard(CreateBoard(account));
            account.AddOrganization(organization);
            return organization;
        }

        public static Board CreateBoard(Account account)
        {
            var board = new Board {Title = "Welcome Board", Administrador = account, IsArchived = false};
            board.AddMember(account);
            account.AddBoard(board);
            board.AddLanes(CreateLane("Basic", 1));
            board.AddLanes(CreateLane("Intermediate", 2));
            board.AddLanes(CreateLane("Advanced", 3));
            return board;
        }

        public static Lane CreateLane(string title, long position)
        {
            var lane = new Lane {Title = title, IsArchived = false, Position = position};
            return lane;
        }

        public static bool IsAValidUpdate(AccountUpdateProfileModel model)
        {
            if (!IsAValidEmail(model.Email))
                throw new BadRequestException("The email is not valid");
            return true;
        }

        public static IRestResponse SendMessage(string email, string name,int n)
        {
            string txt = "";
            if (n == 1)
                txt = "Welcome to MiniTrelloAJDM";
            else
                txt = "You want to change your password";

            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              "key-7c1wwmnzdu06b7b3g55uae5inq455764");
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                "app24266.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "MiniTrelloAJDM <postmaster@sandbox33840.mailgun.org>");
            request.AddParameter("to", email);
            request.AddParameter("subject", "Hello" + name);
            request.AddParameter("text", txt);
            request.Method = Method.POST;
            return client.Execute(request);
        }

    }
}
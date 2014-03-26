using System;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
using MiniTrello.Api.Models;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;
using MiniTrello.Api.Controllers.Helpers;
using RestSharp;

namespace MiniTrello.Api.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IReadOnlyRepository _readOnlyRepository;
        private readonly IWriteOnlyRepository _writeOnlyRepository;
        private readonly IMappingEngine _mappingEngine;

        public AccountController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository,
            IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }

        [HttpPost]
        [POST("login")]
        public AuthenticationModel Login([FromBody] AccountLoginModel model)
        {
            //var account = _readOnlyRepository.First<Account>(account1 => account1.Email == model.Email 
             //   && BCrypt.Net.BCrypt.Verify(model.Password, account1.Password));
            
            var account = _readOnlyRepository.First<Account>(account1 => account1.Email == model.Email 
                && account1.Password == model.Password);

            if (account != null)
            {
                var session = AccountHelpers.CreateNewSession(account);
                var sessionCreated = _writeOnlyRepository.Create(session);
                if (sessionCreated != null)
                    return new AuthenticationModel()
                    {
                        Token = session.Token, 
                        YourSessionExpireIn = session.Duration
                    }; 
            }

            throw new BadRequestException("User or Password is incorrect");
        }

        [POST("register")]
        public SuccessfulMessageResponse Register([FromBody] AccountRegisterModel model)
        {
            var emailExist = _readOnlyRepository.First<Account>(account1 => account1.Email == model.Email);
            if (emailExist == null)
            {
                if (AccountHelpers.IsAValidRegister(model))
                {
                    //string passwordEncode = BCrypt.Net.BCrypt.HashPassword(model.Password, 12);
                    var account = _mappingEngine.Map<AccountRegisterModel, Account>(model);
                    account.IsArchived = false;
                    //account.Password = passwordEncode;
                    account.Password = model.Password;
                    Account accountCreated = _writeOnlyRepository.Create(account);
                    if (accountCreated != null)
                    {
                        AccountHelpers.SendMessage(model.Email, model.FirstName + " " + model.LastName, 1);
                        AccountHelpers.CreateOrganization(accountCreated);
                        return new SuccessfulMessageResponse("You have been registered succesfully");
                    }
                }
                throw new BadRequestException("The Account couldn't be created");
            }
            throw new BadRequestException("The Email is already registered");
        }

        //[AcceptVerbs ("PUT")]
        [POST("updateProfile/{token}")]
        public SuccessfulMessageResponse UpdateProfile([FromBody] AccountUpdateProfileModel model, string token)
        {
            var session = IsTokenExpired(token);
            if (session != null)
            {
                var account = _readOnlyRepository.GetById<Account>(session.User.Id);
                //var updateAccount = _mappingEngine.Map<AccountUpdateProfileModel, Account>(model);
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.UserName = model.UserName;
                account.Initials = model.Initials;
                account.Bio = model.Bio;
                account.Email = model.Email;
                var accountUpdated = _writeOnlyRepository.Update(account);
                if (accountUpdated != null)
                    return new SuccessfulMessageResponse("Your profile was successfully updated");
            }
            throw new BadRequestException("Your profile could not be updated");
        }

        [POST("forgottenPassword")]
        public SuccessfulMessageResponse ChangePassword([FromBody] AccountForgottenPasswordModel model)
        {
            var account = _readOnlyRepository.First<Account>(account1 => account1.Email == model.Email && account1.Password == model.OldPassword);

            if (account != null)
            {
                AccountHelpers.SendMessage(model.Email, account.FirstName + " " + account.LastName, 2);
                if (model.NewPassword != model.ConfirmPassword)
                    throw new BadRequestException("Password and Confirm Password are different");
                
                account.Password = model.NewPassword;
                Account accountCreated = _writeOnlyRepository.Update(account);
                if (accountCreated != null)
                {
                    return new SuccessfulMessageResponse("Your password has been changed");
                }
            }
            throw new BadRequestException("Your email or your password is incorrect");
        }

        public Sessions IsTokenExpired(string token)
        {
            var session = _readOnlyRepository.First<Sessions>(session1 => session1.Token == token);
            if (session.Duration >
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second))
                return session;
            throw new BadRequestException("Your session has expired");
        }
    } 
}


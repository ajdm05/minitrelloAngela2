using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
using FizzWare.NBuilder;
using MiniTrello.Api.Controllers.Helpers;
using MiniTrello.Api.Models;
using MiniTrello.Api.Models.Helper;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;

namespace MiniTrello.Api.Controllers
{
    public class OrganizationController : ApiController
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        readonly IMappingEngine _mappingEngine;

        public OrganizationController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository,
            IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }

        [HttpPost]
        [POST("organizations/create/{token}")]
        public SuccessfulMessageResponse CreateOrganization([FromBody] OrganizationCreationModel model, string token)
        {
            var session = IsTokenExpired(token);
            var account = _readOnlyRepository.First<Account>(account1 => account1.Id == session.User.Id);
            AccountHelpers.CreateOrganizationDefault(account, model.Title, model.Description);
            return new SuccessfulMessageResponse("Organization has been created");
         
        }

        [HttpPost]
        //[AcceptVerbs("DELETE")]
        [POST("organization/delete/{organizationId}/{token}")]
        public SuccessfulMessageResponse Archive(long organizationId, string token)
        {
            var session = IsTokenExpired(token);
            var organization = _readOnlyRepository.GetById<Organization>(organizationId);
            if (organization != null)
            {
                var archivedOrganization = _writeOnlyRepository.Archive(organization);
                return new SuccessfulMessageResponse("Organization has been removed");
            }
            throw new BadRequestException("Organization does not exist");  
        }

        [POST("organizations/addBoard/{token}")]
        public SuccessfulMessageResponse AddBoardToOrganization([FromBody] AddBoardToOrganizationModel model, string token)
        {
            var session = IsTokenExpired(token);
            var account = _readOnlyRepository.First<Account>(account1 => account1.Id == session.User.Id);
            var organization = _readOnlyRepository.GetById<Organization>(model.Organization_id);
            if (organization != null)
            {
                var board = _mappingEngine.Map<AddBoardToOrganizationModel, Board>(model);
                board.Administrador = account;
                board.AddLanes(AccountHelpers.CreateLane("Basic",1));
                board.AddLanes(AccountHelpers.CreateLane("Intermediate", 2));
                board.AddLanes(AccountHelpers.CreateLane("Advanced", 3));
                organization.AddBoard(board);
                return new SuccessfulMessageResponse("Organization has been created");
            }
            throw new BadRequestException("Board could not be added");
        }

        [GET("organizations/{token}")]
         public List<OrganizationModel> GetAllForUser(string token)
         {
             var session = IsTokenExpired(token);
             var account = _readOnlyRepository.GetById<Account>(session.User.Id);
             if (account != null)
             {
                 var organizations = new List<OrganizationModel>();
                 foreach (var member in account.Organizations)
                 {
                     if (member.IsArchived == false)
                     {
                         var myOrganizations = _mappingEngine.Map<Organization, OrganizationModel>(member);
                         organizations.Add(myOrganizations);
                     }  
                 }
                 return organizations;
             }
             throw new BadRequestException("You can't see the members of this Board");

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
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

        [POST("/organizations/create/{token}")]
        public SuccessfulMessageResponse CreateOrganization([FromBody] OrganizationCreationModel model, string token)
        {
            var session = IsTokenExpired(token);
            var account = _readOnlyRepository.First<Account>(account1 => account1.Id == session.User.Id);
            AccountHelpers.CreateOrganizationDefault(account, model.Title, model.Description);
            return new SuccessfulMessageResponse("Organization has been created");
         
        }

        [POST("/organizations/addBoard/{token}")]
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
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
    public class LaneController: ApiController
    {
        private readonly IReadOnlyRepository _readOnlyRepository;
        private readonly IWriteOnlyRepository _writeOnlyRepository;
        private readonly IMappingEngine _mappingEngine;

        public LaneController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository,
            IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }

        [POST("/lanes/create/{boardId}/{token}")]
        public SuccessfulMessageResponse CreateLane([FromBody] LanesCreationModel model, string token, long boardId)
        {
            var session = IsTokenExpired(token);
            var boardToAddLane1 = _readOnlyRepository.GetById<Board>(boardId);
            if (boardToAddLane1 != null)
            {
                var laneToAdd = _mappingEngine.Map<LanesCreationModel, Lane>(model);
                laneToAdd.IsArchived = false;
                laneToAdd.Position = boardToAddLane1.Lanes.Count()+1;
                boardToAddLane1.AddLanes(laneToAdd);
                Lane laneCreated = _writeOnlyRepository.Create(laneToAdd);
                if (laneCreated != null)
                    return new SuccessfulMessageResponse("Lane was succesfully created");
            }  
            throw new BadRequestException("Lane could not be created");
        }

        [GET("lanes/{boardId}/{token}")]
        public List<LaneModel> GetAllForUser(string token, long boardId)
        {
            var session = IsTokenExpired(token);
            var board = _readOnlyRepository.GetById<Board>(boardId);
            if (board != null)
            {
                var lanes = new List<LaneModel>();
                foreach (var member in board.Lanes)
                {
                    if (member.IsArchived == false)
                    {
                        var myLanes = _mappingEngine.Map<Lane, LaneModel>(member);
                        lanes.Add(myLanes);
                    }
                }
                return lanes;
            }
            throw new BadRequestException("You can't see your Boards");

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
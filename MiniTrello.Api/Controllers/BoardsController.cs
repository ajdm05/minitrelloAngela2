using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
using Microsoft.SqlServer.Server;
using MiniTrello.Api.Controllers.Helpers;
using MiniTrello.Api.Models;
using MiniTrello.Api.Models.Helper;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;

namespace MiniTrello.Api.Controllers
{
    public class BoardsController : ApiController
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        readonly IMappingEngine _mappingEngine;

        public BoardsController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository,
            IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }

        [POST("/boards/addmember/{token}")]
        public BoardModel AddMemberToBoard([FromBody] AddMemberBoardModel model, string token)
        {
            var session = IsTokenExpired(token);
            var memberToAdd = _readOnlyRepository.GetById<Account>(model.MemberId);
            var board = _readOnlyRepository.GetById<Board>(model.BoardId);
            if (board != null && memberToAdd != null)
            {
                board.AddMember(memberToAdd);
                memberToAdd.AddBoard(board);
                var updateBoard = _writeOnlyRepository.Update(board);
                var boardModel = _mappingEngine.Map<Board, BoardModel>(updateBoard);
                string activityDone = "Add " + memberToAdd.FirstName + " " + memberToAdd.LastName;
                board.AddActivity(ActivityHelper.CreateActivity(session.User, activityDone));
                return boardModel;
            }
            throw  new BadRequestException("Member or Board does not exist");
        }

        [POST("/boards/create/{token}")]
        public BoardModel CreateBoard([FromBody] BoardsCreationModel model, string token)
        {
            var session = IsTokenExpired(token);
            var account = _readOnlyRepository.First<Account>(account1 => account1.Id == session.User.Id);
            var boardToAdd = _mappingEngine.Map<BoardsCreationModel, Board>(model);
            boardToAdd.Administrador = account;
            boardToAdd.AddMember(account);
            boardToAdd.IsArchived = false;
            account.AddBoard(boardToAdd);
            Board boardCreated = _writeOnlyRepository.Create(boardToAdd);
            if (boardCreated != null)
            {
                string activityDone = "Add " + boardToAdd.Title;
                boardToAdd.AddActivity(ActivityHelper.CreateActivity(session.User, activityDone));
                return new BoardModel{ Title = boardCreated.Title, Id = boardCreated.Id};
            }
            throw new BadRequestException("The board could not be created");
        }

        [POST("/boards/rename/{token}")]
        public SuccessfulMessageResponse RenameBoard([FromBody] RenameBoardModel model, string token)
        {
            var session = IsTokenExpired(token);
            var account = _readOnlyRepository.First<Account>(account1 => account1.Id == session.User.Id);
            var boardToRename = _readOnlyRepository.First<Board>(board1 => board1.Id == model.BoardToRename);
            if (boardToRename != null && boardToRename.Administrador == account)
            {
                boardToRename.Title = model.NewTitle;
                Board boardCreated = _writeOnlyRepository.Update(boardToRename);
                if (boardCreated != null)
                {
                    string activityDone = "Changed " + model.BoardToRename + " for "+ model.NewTitle;
                    boardToRename.AddActivity(ActivityHelper.CreateActivity(session.User, activityDone));
                    return new SuccessfulMessageResponse("The board has been renamed");
                }
                throw new BadRequestException("You can't change the Title");
            }

            throw new BadRequestException("You aren't the administrator of the Board");
            
        }

        [POST("/boards/delet/{token}")]
        public SuccessfulMessageResponse DeletBoard([FromBody] BoardsRemovedModel model, string token)
        {
            var session = IsTokenExpired(token);
            var account = _readOnlyRepository.GetById<Account>(session.User.Id);
            var boardToRemove = _readOnlyRepository.GetById<Board>(model.Id);
            if (boardToRemove != null && boardToRemove.Administrador == account)
            {    
                Board boardDeleted = _writeOnlyRepository.Archive(boardToRemove);
                if (boardDeleted != null)
                {
                    string activityDone = "Deleted " + boardToRemove.Title ;
                    boardToRemove.AddActivity(ActivityHelper.CreateActivity(session.User, activityDone));
                    return new SuccessfulMessageResponse("The board has been deleted");
                }      
            }
            throw new BadRequestException("You aren't the administrator of the Board");
        }

        [POST("/boards/boardMembers/{token}")]
        public BoardMembersModel MembersList([FromBody] BoardsMembersList model, string token)
        {
            var session = IsTokenExpired(token);
            var boardToSee = _readOnlyRepository.First<Board>(board1 => board1.Id == model.BoardId);
            if (boardToSee != null)
            {
                var memberS = new BoardMembersModel();
                foreach (var member in boardToSee.Members)
                {
                    var memberToAdd = new AccountModel();
                    memberToAdd.Id = member.Id;
                    memberToAdd.FirstName = member.FirstName;
                    memberToAdd.LastName = member.LastName;
                    memberS.Members.Add(memberToAdd);
                }
                return memberS;
            }
            throw new BadRequestException("You can't see the members of this Board");

        }

        [GET("/boards/{token}")]
        public MyBoardsModel MyBoards(string token)
        {
            var session = IsTokenExpired(token);
            var account = _readOnlyRepository.GetById<Account>(session.User.Id);
            if (account != null)
            {
                var boards = new MyBoardsModel();
                foreach (var member in account.Boards)
                {
                    var myBoards = _mappingEngine.Map<Board, BoardModel>(member);
                    boards.Members.Add(myBoards);
                }
                return boards;
            }
            throw new BadRequestException("You can't see the members of this Board");
         
        }

        [POST("/boards/{BoardId}/{token}")]
        public BoardDetailsModel BoardsDetails(string token, long BoardId)
        {
            var session = IsTokenExpired(token);
            var board = _readOnlyRepository.GetById<Board>(BoardId);
            if (board != null)
            {
                var boardDetails = _mappingEngine.Map<Board, BoardDetailsModel>(board);
                if (boardDetails != null)
                {
                    foreach (var lane in board.Lanes)
                    {
                        var myLane = _mappingEngine.Map<Lane, LaneModel>(lane);
                        boardDetails.myLanes.Add(myLane);
                    }
                }
                    return boardDetails;
            }
            throw new BadRequestException("You can't see the details of this Board");
        }

        [POST("/boards/activities/{token}")]
        public ListActivities BoardsActivities([FromBody] ListMyActivitiesBoardModel model, string token)
        {
            var session = IsTokenExpired(token);
            var board = _readOnlyRepository.GetById<Board>(model.Id);
            if (board != null)
            {
                var activities = new ListActivities();
                /*foreach (var activity in board.Activities)
                {
                    var myActivity = new ActivityModel();
                    myActivity.Id = activity.Id;
                    myActivity.WhenHadDone = activity.WhenHadDone;
                    myActivity.FirstName = activity.FirstName;
                    myActivity.LastName = activity.LasttName;
                    //var myActivity = _mappingEngine.Map<Activity, ActivityModel>(activity);
                    activities.ListActivity.Add(myActivity);
                }*/
                return activities;
            }
            throw new BadRequestException("You can't see the details of this Board");
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
        public class AddMemberBoardModel
        {
            public long MemberId { get; set; }
            public long BoardId { get; set; }
        }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class CardsController : ApiController
    {
        private readonly IReadOnlyRepository _readOnlyRepository;
        private readonly IWriteOnlyRepository _writeOnlyRepository;
        private readonly IMappingEngine _mappingEngine;

        public CardsController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository,
            IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }


        [POST("/cards/create/{token}")]
        public SuccessfulMessageResponse CreateCard([FromBody] CardsCreationModel model, string token)
        {
            var session = IsTokenExpired(token);
            var laneToAddCard = _readOnlyRepository.GetById<Lane>(model.Lane_id);
            var board = _readOnlyRepository.First<Board>(board1 => board1.Lanes.Contains(laneToAddCard));
            var account = _readOnlyRepository.First<Account>(account1 => account1.Id == session.User.Id);
            if (laneToAddCard != null && account != null)
            {
                var cardToAdd = _mappingEngine.Map<CardsCreationModel, Card>(model);
                cardToAdd.IsArchived = false;
                cardToAdd.Position = laneToAddCard.Cards.Count() + 1;
                cardToAdd.AddMember(account);
                account.AddCard(cardToAdd);
                laneToAddCard.AddCards(cardToAdd);
                Card cardCreated = _writeOnlyRepository.Create(cardToAdd);
                if (cardCreated != null)
                {
                    string activityDone = "Add " + cardCreated.Text + " to " + board.Title;
                    board.AddActivity(ActivityHelper.CreateActivity(session.User, activityDone));
                    return new SuccessfulMessageResponse("Lane was successfully created");
                }
            }
            throw new BadRequestException("YThis Lane does not exist");
          
        }

        [POST("/cards/remove/{token}")]
        public SuccessfulMessageResponse DeletCard([FromBody] CardRemoveModel model, string token)
        {
            var session = IsTokenExpired(token);
            var cardToRemove = _readOnlyRepository.GetById<Card>(model.Card_id);
            var lane = _readOnlyRepository.First<Lane>(lane1 => lane1.Cards.Contains(cardToRemove));
            var board = _readOnlyRepository.First<Board>(board1 => board1.Lanes.Contains(lane));     
            if (cardToRemove != null)
            {
                Card cardDeleted = _writeOnlyRepository.Archive(cardToRemove);
                if (cardDeleted != null)
                {
                    string activityDone = "Deleted " + cardDeleted.Text + " from " + lane.Title;
                    board.AddActivity(ActivityHelper.CreateActivity(session.User, activityDone));
                    return new SuccessfulMessageResponse("Card was successfully removed");
                }
            }
            throw new BadRequestException("Card could not be removed");   
        }

        [GET("cards/{laneId}/{token}")]
        public List<CardModel> GetAllForUser(string token, long laneId)
        {
            var session = IsTokenExpired(token);
            var lane = _readOnlyRepository.GetById<Lane>(laneId);
            if (lane != null)
            {
                var cards = new List<CardModel>();
                foreach (var member in lane.Cards)
                {
                    if (member.IsArchived == false)
                    {
                        var myCards = _mappingEngine.Map<Card, CardModel>(member);
                        cards.Add(myCards);
                    }
                }
                return cards;
            }
            throw new BadRequestException("You can't see your Boards");

        }
        [POST("/cards/move/{token}")]
        public SuccessfulMessageResponse MoveCard([FromBody] CardMovedModel model, string token)
        {
            var session = IsTokenExpired(token);
            var cardToMove = _readOnlyRepository.GetById<Card>(model.CardToMove_id);
            var laneFromMove = _readOnlyRepository.GetById<Lane>(model.OldLane_id);
            var laneToMove = _readOnlyRepository.GetById<Lane>(model.NewLane_id);
            var lane = _readOnlyRepository.First<Lane>(lane1 => lane1.Cards.Contains(cardToMove));
            var board = _readOnlyRepository.First<Board>(board1 => board1.Lanes.Contains(laneToMove));
            if (laneFromMove != null && laneToMove != null && cardToMove != null)
            {
                laneFromMove.Cards.ToList().Remove(cardToMove);
                laneToMove.AddCards(cardToMove);
                Lane laneFromMoveUpdated = _writeOnlyRepository.Update(laneFromMove);
                Lane laneToMoveUpdated = _writeOnlyRepository.Update(laneToMove);
                cardToMove.Position = 5;
                laneToMove.AddCards(cardToMove);
                Card cardMoved = _writeOnlyRepository.Update(cardToMove);
                if (cardMoved != null && laneFromMoveUpdated != null && laneToMoveUpdated != null)
                {
                    string activityDone = "Moved " + cardMoved.Text + " from " + laneFromMove.Title + " to " + laneToMove.Title;
                    board.AddActivity(ActivityHelper.CreateActivity(session.User, activityDone));
                    return new SuccessfulMessageResponse("Card was successfully moved");
                }
            }
            throw new BadRequestException("Card could not be moved");
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
using System;
using System.Web.Providers.Entities;
using AutoMapper;
using MiniTrello.Api.Controllers;
using MiniTrello.Api.Models;
using MiniTrello.Domain.Entities;
using MiniTrello.Infrastructure;

namespace MiniTrello.Api
{
    public class ConfigureAutomapper : IBootstrapperTask
    {
        public void Run()
        {
            Mapper.CreateMap<Account, AccountLoginModel>().ReverseMap();
            Mapper.CreateMap<Account, AccountRegisterModel>().ReverseMap();
            Mapper.CreateMap<Sessions, SessionModel>().ReverseMap();
            Mapper.CreateMap<Board, BoardsCreationModel>().ReverseMap();
            Mapper.CreateMap<Organization, OrganizationCreationModel>().ReverseMap();
            Mapper.CreateMap<OrganizationModel, Organization>().ReverseMap();
            Mapper.CreateMap<Organization, OrganizationModel>().ReverseMap();
            Mapper.CreateMap<Account, AccountUpdateProfileModel>().ReverseMap();
            Mapper.CreateMap<Lane, LanesCreationModel>().ReverseMap();
            Mapper.CreateMap<Card, CardsCreationModel>().ReverseMap();
            Mapper.CreateMap<BoardModel, Board>().ReverseMap();
            Mapper.CreateMap<AccountModel, Account>().ReverseMap();
            Mapper.CreateMap<MyBoardsModel, Board>().ReverseMap();
            Mapper.CreateMap<BoardDetailsModel, Board>().ReverseMap();
            Mapper.CreateMap<LaneModel, Lane>().ReverseMap();
            Mapper.CreateMap<CardModel, Card>().ReverseMap();
            Mapper.CreateMap<Board, AddBoardToOrganizationModel>().ReverseMap();
            Mapper.CreateMap<ActivityModel, Activity>().ReverseMap();
            //Mapper.CreateMap<DemographicsEntity, DemographicsModel>().ReverseMap();
            //Mapper.CreateMap<IReportEntity, IReportModel>()
            //    .Include<DemographicsEntity, DemographicsModel>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniTrello.Api.Models;
using MiniTrello.Domain.DataObjects;
using MiniTrello.Domain.Entities;
using RestSharp;

namespace MiniTrello.ApiWrapper
{
    public class MiniTrelloSdk
    {
        private static RestRequest InitRequest(string resource, Method method,object payload)
        {
            var request = new RestRequest(resource, method);
            request.AddHeader("Content-Type", "application/json");
            request.AddBody(payload);
            return request;
        }

        public static AuthenticationModel Login(AccountLoginModel loginModel)
        {
                var client = new RestClient(BaseUrl);
                var request = InitRequest("/login", Method.POST, loginModel);
                IRestResponse<AuthenticationModel> response = client.Execute<AuthenticationModel>(request);
                ConfigurationManager.AppSettings["accessToken"] = response.Data.Token;
                return response.Data;
        }

        public static AccountRegisterModel Register(AccountRegisterModel registerModel)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/register", Method.POST, registerModel);
            IRestResponse<AccountRegisterModel> response = client.Execute<AccountRegisterModel>(request);
            return response.Data;
        }

        public static AccountForgottenPasswordModel ForgotPassword(AccountForgottenPasswordModel forgotPasswordModel)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/forgottenPassword", Method.PUT, forgotPasswordModel);
            IRestResponse<AccountForgottenPasswordModel> response = client.Execute<AccountForgottenPasswordModel>(request);
            return response.Data;
        }

        public static AccountUpdateProfileModel UpdateProfile(AccountUpdateProfileModel updateProfileModel, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/updateProfile/" + token, Method.PUT, updateProfileModel);
            IRestResponse<AccountUpdateProfileModel> response = client.Execute<AccountUpdateProfileModel>(request);
            return response.Data;
        }

        public static List<BoardModel> GetBoards(MyBoardsModel boardModel, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/boards/myBoards/" + token, Method.POST, boardModel);
            IRestResponse<MyBoardsModel> response = client.Execute<MyBoardsModel>(request);
            return response.Data.Members;
        }

        public static List<AccountModel> GetBoardMembers(BoardMembersModel boardMembersModel, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/boards/boardMembers/" + token, Method.POST, boardMembersModel);
            IRestResponse<BoardMembersModel> response = client.Execute<BoardMembersModel>(request);
            return response.Data.Members;
        }

        public static BoardDetailsModel BoardDetails(long idBoard, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/boards/" + idBoard + "/" + token, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse<BoardDetailsModel> response = client.Execute<BoardDetailsModel>(request);
            return response.Data;
        }


        public static BoardModel RenameBoard(RenameBoardModel boardRename, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/boards/rename/" + token, Method.PUT, boardRename);
            IRestResponse<BoardModel> response = client.Execute<BoardModel>(request);
            return response.Data;
        }
        public static BoardModel DeletBoard(BoardsRemovedModel boardRemove, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/boards/delet/" + token, Method.DELETE, boardRemove);
            IRestResponse<BoardModel> response = client.Execute<BoardModel>(request);
            return response.Data;
        }
        public static BoardModel CreateBoard(BoardsCreationModel boardCreation, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/boards/addMember/" + token, Method.POST, boardCreation);
            IRestResponse<BoardModel> response = client.Execute<BoardModel>(request);
            return response.Data;
        }
        public static ListActivities BoardActivities(ListMyActivitiesBoardModel boardActivities, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/boards/activities/" + token, Method.POST, boardActivities);
            IRestResponse<ListActivities> response = client.Execute<ListActivities>(request);
            return response.Data;
        }

        public static CardModel CreateCard(CardsCreationModel cardCreate, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/cards/create/" + token, Method.POST, cardCreate);
            IRestResponse<CardModel> response = client.Execute<CardModel>(request);
            return response.Data;
        }
        public static CardModel RemoveCard(CardRemoveModel cardRemove, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/cards/remove/" + token, Method.DELETE, cardRemove);
            IRestResponse<CardModel> response = client.Execute<CardModel>(request);
            return response.Data;
        }
        public static CardModel MoveCard(CardMovedModel cardMoved, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/cards/move/" + token, Method.POST, cardMoved);
            IRestResponse<CardModel> response = client.Execute<CardModel>(request);
            return response.Data;
        }
        public static LaneModel CreateLane(LanesCreationModel laneCreate, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/lanes/create/" + token, Method.POST, laneCreate);
            IRestResponse<LaneModel> response = client.Execute<LaneModel>(request);
            return response.Data;
        }
        public static OrganizationModel CreateOrganization(OrganizationCreationModel organizationCreation, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/organizations/create/" + token, Method.POST, organizationCreation);
            IRestResponse<OrganizationModel> response = client.Execute<OrganizationModel>(request);
            return response.Data;
        }
        public static OrganizationModel AddBoardToOrganization(AddBoardToOrganizationModel organizationAddBoard, string token)
        {
            var client = new RestClient(BaseUrl);
            var request = InitRequest("/organizations/addBoard/" + token, Method.POST, organizationAddBoard);
            IRestResponse<OrganizationModel> response = client.Execute<OrganizationModel>(request);
            return response.Data;
        } 
        private static string BaseUrl
        {
            get { return ConfigurationManager.AppSettings["baseUrl"]; }
        }

        public static List<OrganizationModel> GetOrganization(OrganizationModel organizationModel)
        {
            return null;        
        } 

        
    }
}

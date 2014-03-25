using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniTrello.Api.Models
{
    public class OrganizationListModel
    {
        public List<AccountBoardModel> Boards { get; set; }
    }
}
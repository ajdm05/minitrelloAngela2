using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniTrello.Domain.Entities;

namespace MiniTrello.Api.Models
{
    public class BoardDetailsModel
    {
        public List<LaneModel> myLanes = new List<LaneModel>();
        public long Id { get; set; }
        public string Title { get; set; }
        public long Administrador_id { get; set; }
        public bool IsArchive { get; set; }
    }
}
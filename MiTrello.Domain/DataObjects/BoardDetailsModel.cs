using System.Collections.Generic;

namespace MiniTrello.Domain.DataObjects
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
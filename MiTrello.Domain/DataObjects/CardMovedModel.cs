using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniTrello.Api.Models
{
    public class CardMovedModel
    {
        public long OldLane_id { get; set; }
        public long NewLane_id { get; set; }
        public long CardToMove_id { get; set; }
    }
}
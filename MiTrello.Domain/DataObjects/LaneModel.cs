using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniTrello.Api.Models
{
    public class LaneModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long Position { get; set; }
        public bool IsArchive { get; set; }
    }
}
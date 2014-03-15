using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniTrello.Api.Models
{
    public class CardsCreationModel
    {
        public string Text { get; set; }
        public long Lane_id { get; set; }
    }
}
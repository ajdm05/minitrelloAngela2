using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniTrello.Domain.Entities;

namespace MiniTrello.Api.Models
{
    public class LanesCreationModel
    {
        public long Board_id { get; set; }
        public string Title { get; set; }
    }
}
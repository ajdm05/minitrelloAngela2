using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniTrello.Api.Models
{
    public class ActivityModel
    {
        public long Id { get; set; }
        public DateTime WhenHadDone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
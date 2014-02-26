using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniTrello.Api.Models
{
    public class ActivityDoneModel
    {
        public virtual string ActivityDone { get; set; }
        public virtual DateTime WhenHadDone { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LasttName { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
    }
}
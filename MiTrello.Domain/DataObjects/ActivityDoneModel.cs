using System;

namespace MiniTrello.Domain.DataObjects
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
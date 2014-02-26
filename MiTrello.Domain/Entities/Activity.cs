using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrello.Domain.Entities
{
    public class Activity : IEntity
    {
        public virtual string ActivityDone { get; set; }
        public virtual DateTime WhenHadDone { get; set; }
        public virtual Account User { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LasttName { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
    }
}

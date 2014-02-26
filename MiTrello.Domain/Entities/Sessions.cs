using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrello.Domain.Entities
{
    public class Sessions : IEntity
    {
        public virtual Account User { get; set; }
        public virtual DateTime Creation { get; set; }
        public virtual DateTime Duration { get; set; }
        public virtual string Token { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
    }
}

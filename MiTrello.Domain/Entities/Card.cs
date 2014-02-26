using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrello.Domain.Entities
{
    public class Card : IEntity
    {
        private readonly IList<Account> _members = new List<Account>();
        public virtual string Text { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual long Position { get; set; }
        public virtual IEnumerable<Account> Members
        {
            get { return _members; }
        }
        public virtual void AddMember(Account member)
        {
            _members.Add(member);
        }
    }
}

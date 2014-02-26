using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrello.Domain.Entities
{
    public class Lane : IEntity
    {
        public virtual string Title { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual long Position { get; set; }

        private readonly IList<Card> _cards = new List<Card>();

        public virtual IEnumerable<Card> Cards
        {
            get { return _cards; }
        }
        public virtual void AddCards(Card card)
        {
            _cards.Add(card);
        }
    }
}

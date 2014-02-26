using System.Collections.Generic;

namespace MiniTrello.Domain.Entities
{
    public class Account : IEntity
    {
        private readonly IList<Board> _boards = new List<Board>();
        private readonly IList<Organization> _organizations = new List<Organization>();
        private readonly IList<Card> _cards = new List<Card>();
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Bio { get; set; }
        public virtual string Initials { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual IEnumerable<Board> Boards
        {
            get { return _boards; }
        }
        public virtual void AddBoard(Board board)
        {
            if (!_boards.Contains(board))
            {
                _boards.Add(board);
            }
        }
        public virtual IEnumerable<Organization> Organizations
        {
            get { return _organizations; }
        }
        public virtual void AddOrganization(Organization organization)
        {
            if (!_organizations.Contains(organization))
            {
                _organizations.Add(organization);
            }
        }
        public virtual IEnumerable<Card> Cards
        {
            get { return _cards; }
        }
        public virtual void AddCard(Card card)
        {
            if (!_cards.Contains(card))
            {
                _cards.Add(card);
            }
        }

    }
}
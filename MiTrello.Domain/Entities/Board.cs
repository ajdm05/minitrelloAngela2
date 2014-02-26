using System.Collections.Generic;

namespace MiniTrello.Domain.Entities
{
    public class Board : IEntity    
    {
        public virtual Account Administrador { get; set; }
        private readonly IList<Account> _members = new List<Account>();
        private readonly IList<Lane> _lanes = new List<Lane>();
        private readonly IList<Activity> _activities = new List<Activity>();
        public virtual string Title { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual IEnumerable<Lane> Lanes
        {
            get { return _lanes; }
        }
        public virtual void AddLanes(Lane lane)
        {
            _lanes.Add(lane);
        }
        public virtual IEnumerable<Account> Members
        {
            get { return _members; }
        }
        public virtual void AddMember(Account member)
        {
            _members.Add(member);
        }
        public virtual IEnumerable<Activity> Activities
        {
            get { return _activities; }
        }
        public virtual void AddActivity(Activity activity)
        {
            _activities.Add(activity);
        }
    }
}
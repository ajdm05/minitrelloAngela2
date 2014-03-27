namespace MiniTrello.Domain.DataObjects
{
    public class CardMovedModel
    {
        public long OldLane_id { get; set; }
        public long NewLane_id { get; set; }
        public long CardToMove_id { get; set; }
    }
}
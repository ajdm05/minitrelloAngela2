namespace MiniTrello.Domain.DataObjects
{
    public class LaneModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long Position { get; set; }
        public bool IsArchive { get; set; }
    }
}
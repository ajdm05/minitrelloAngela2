namespace MiniTrello.Domain.DataObjects
{
    public class RenameBoardModel
    {
        public long BoardToRename { get; set; }
        public string NewTitle { get; set; }
    }
}
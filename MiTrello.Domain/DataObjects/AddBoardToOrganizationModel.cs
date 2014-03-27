namespace MiniTrello.Domain.DataObjects
{
    public class AddBoardToOrganizationModel
    {
        public long Organization_id { get; set; }
        public string Title { get; set; }
    }
}
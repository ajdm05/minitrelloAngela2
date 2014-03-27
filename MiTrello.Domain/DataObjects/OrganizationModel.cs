namespace MiniTrello.Domain.DataObjects
{
    public class OrganizationModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }
    }
}
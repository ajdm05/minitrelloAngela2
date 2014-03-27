using System;

namespace MiniTrello.Domain.DataObjects
{
    public class ActivityModel
    {
        public long Id { get; set; }
        public DateTime WhenHadDone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
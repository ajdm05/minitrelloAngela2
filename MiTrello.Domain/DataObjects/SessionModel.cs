using System;
using MiniTrello.Domain.Entities;

namespace MiniTrello.Domain.DataObjects
{
    public class SessionModel
    {
        public  Account User { get; set; }
        public  DateTime Creation { get; set; }
        public  DateTime Duration { get; set; }
        public  string Token { get; set; }
        public long Id { get; set; }
        public bool IsArchived { get; set; }
    }
}
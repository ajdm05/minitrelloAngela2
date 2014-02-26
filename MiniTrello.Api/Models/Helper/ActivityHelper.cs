using System;
using MiniTrello.Domain.Entities;

namespace MiniTrello.Api.Models.Helper
{
    public class ActivityHelper
    {
        public static Activity CreateActivity(Account user, string activityDonde)
        {
            var activity = new Activity
            {
                ActivityDone = activityDonde,
                WhenHadDone = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                User = user,
                FirstName = user.FirstName,
                LasttName = user.LastName,
                IsArchived = false
            };
            
            return activity;
        }
    }
}
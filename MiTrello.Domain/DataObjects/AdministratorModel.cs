using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniTrello.Api.Models
{
    public class AdministratorModel
    {
        public long Id { get; set; }
        public string AdministratorFirstName { get; set; }
        public string AdministratorLastName { get; set; }
    }
}
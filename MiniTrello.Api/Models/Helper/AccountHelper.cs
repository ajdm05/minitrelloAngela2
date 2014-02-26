using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using MiniTrello.Domain.Entities;

namespace MiniTrello.Api.Models.Helper
{
    public class AccountHelper
    {
        private bool _invalid;
        public bool IsValidEmail(string strIn)
        {
            _invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                                    RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException) {
                return false;
            }

            if (_invalid) 
                return false;

            // Return true if strIn is in valid e-mail format.
            try {
                return Regex.IsMatch(strIn, 
                    @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + 
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$", 
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }  
            catch (RegexMatchTimeoutException) {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            var idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                _invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        public static Board CreateBoard(Account account)
        {
            var board = new Board {Title = "Welcome Board", Administrador = account, IsArchived = false};
            board.AddMember(account);
            account.AddBoard(board);
            board.AddLanes(CreateLane("Basic",1));
            board.AddLanes(CreateLane("Intermediate", 2));
            board.AddLanes(CreateLane("Advanced", 3));
            return board;
        }

        public static Organization CreateOrganization(Account account)
        {
            var organization = new Organization { Title = "Welcome Organization", Administrador = account, Description = "This is your first Organization", IsArchived = false };
            organization.AddMember(account);
            account.AddOrganization(organization);
            return organization;
        }

        private static Lane CreateLane(string title, long position)
        {
            var lane = new Lane {Title = title, IsArchived = false, Position = position};
            return lane;
        }
        public static bool IsValidAFormat(String email, string expresion)
        {
            if (Regex.IsMatch(email, expresion))
            {
                return Regex.Replace(email, expresion, String.Empty).Length == 0;
            }
            return false;
        }

        public static bool EmailIsValid(String email)
        {
            const string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                return Regex.Replace(email, expresion, String.Empty).Length == 0;
            }
            return false;
        }

        public static bool PasswordIsCorrect(String password)
        {
            const string expresion = "^(.{0,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{4,})|(.{1,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{3,})|(.{2,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{2,})|(.{3,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{1,})|(.{4,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{0,})$";
            if (Regex.IsMatch(password, expresion))
            {
                return Regex.Replace(password, expresion, String.Empty).Length == 0;
            }
            return false;
        }

        public bool NameIsCorrect(string name)
        {
            const string expresion = "^(.{0,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{4,})|(.{1,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{3,})|(.{2,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{2,})|(.{3,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{1,})|(.{4,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{0,})$";
            if (Regex.IsMatch(name, expresion))
            {
                return Regex.Replace(name, expresion, String.Empty).Length == 0;
            }
            return false;
        }

        public bool LastNameIsCorrect(string lastName)
        {
            const string expresion = "^(.{0,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{4,})|(.{1,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{3,})|(.{2,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{2,})|(.{3,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{1,})|(.{4,}(([a-zA-Z][^a-zA-Z])|([^a-zA-Z][a-zA-Z])).{0,})$";
            if (Regex.IsMatch(lastName, expresion))
            {
                return Regex.Replace(lastName, expresion, String.Empty).Length == 0;
            }
            return false;
        }

        public static bool IsTokenExpired(DateTime duration)
        {
            if (duration >
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second))
                return false;
            return true;
        }

        public static long LastPositionOfLanes(Board board)
        {
            return board.Lanes.Max().Position;
        }
    }
}
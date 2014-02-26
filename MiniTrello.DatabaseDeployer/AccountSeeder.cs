using System.Collections.Generic;
using DomainDrivenDatabaseDeployer;
using FizzWare.NBuilder;
using MiniTrello.Api.Controllers.Helpers;
using MiniTrello.Api.Models.Helper;
using MiniTrello.Domain.Entities;
using NHibernate;

namespace MiniTrello.DatabaseDeployer
{
    public class AccountSeeder : IDataSeeder
    {
        readonly ISession _session;

        public AccountSeeder(ISession session)
        {
            _session = session;
        }

       /* public void Seed()
        {
            IList<Account> accountList = Builder<Account>.CreateListOfSize(10).Build();
            foreach (Account account in accountList)
            {
                var boards = Builder<Board>.CreateListOfSize(2).Build();
                foreach (var board in boards)
                {
                    _session.Save(board);
                }
                account.AddBoard(boards[0]);
                account.AddBoard(boards[1]);
                _session.Save(account);
            }
            var account1 = new Account();
            

        }*/
        public void Seed()
        {
            var acount = new Board();
            var accountList = (IList<Account>) acount.Members;
            foreach (Account account in accountList)
            {
                account.AddBoard(AccountHelpers.CreateBoard(account));
                _session.Save(account);
            }
        }
    }
}
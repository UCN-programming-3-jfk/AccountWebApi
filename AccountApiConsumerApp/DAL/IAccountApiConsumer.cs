using System.Collections.Generic;

namespace AccountApiConsumerApp.DAL
{
    public interface IAccountApiConsumer
    {
        string ServiceUri { get; }
        Account AddAccount(Account accountToAdd);
        void DeleteAccount(int idOfAccountToDelete);
        IEnumerable<Account> GetAllAccounts();
    }
}
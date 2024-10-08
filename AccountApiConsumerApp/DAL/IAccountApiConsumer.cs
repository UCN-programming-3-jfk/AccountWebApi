using System.Collections.Generic;

namespace AccountApiConsumerApp.DAL;

public interface IAccountApiConsumer
{
    Account AddAccount(Account accountToAdd);
    void DeleteAccount(int idOfAccountToDelete);
    IEnumerable<Account> GetAllAccounts();
}
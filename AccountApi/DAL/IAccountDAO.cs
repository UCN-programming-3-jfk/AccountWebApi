using System.Collections.Generic;

namespace AccountApi.DAL;
public interface IAccountDAO
{
    Account? Get(int id);
    IEnumerable<Account> GetAll();
    int Insert(Account account);
    bool Update(Account account);
    bool Delete(int id);
}
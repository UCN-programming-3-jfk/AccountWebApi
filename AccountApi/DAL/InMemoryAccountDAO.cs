using System.Collections.Generic;

namespace AccountApi.DAL
{
    public class InMemoryAccountDAO : IAccountDAO
    {
        #region Properties
        private List<Account> accounts = new List<Account>();
        private int nextId = 1;
        #endregion

        #region CRUD methods

        public int Insert(Account account)
        {
            account.Id = GetNextIdAndIncrement();
            accounts.Add(account);
            return account.Id;
        }

        public bool Delete(int id)
        {
            for (int accountCounter = accounts.Count - 1; accountCounter >= 0; accountCounter--)
            {
                if (accounts[accountCounter].Id == id)
                {
                    accounts.RemoveAt(accountCounter);
                    return true;
                }
            }
            return false;
        }

        public Account? Get(int id)
        {
            foreach (var account in accounts)
            {
                if (account.Id == id)
                {
                    return account;
                }
            }
            return null;
        }

        public IEnumerable<Account> GetAll()
        {
            return new List<Account>(accounts);
        }

        public bool Update(Account account)
        {
            Account? accountToUpdate = Get(account.Id);
            if (accountToUpdate == null) { return false; }
            accountToUpdate.Name = account.Name;
            accountToUpdate.Balance = account.Balance;
            return true;
        }

        #endregion

        #region Helper methods
        private int GetNextIdAndIncrement()
        {
            return nextId++;
        } 
        #endregion
    }
}
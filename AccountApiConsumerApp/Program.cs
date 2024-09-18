using AccountApiConsumerApp.DAL;

namespace AccountApiConsumerApp;

internal class Program
{
    public static void Main()
    {
        var localURI = "https://localhost:7216/api/v1/accounts";
        AccountApiConsumer consumer = new AccountApiConsumer(localURI);

        Account account1 = new Account() { Name = "My first savings account", Balance = 30 };
        Account account2 = new Account() { Name = "My second savings account", Balance = 50 };

        Console.WriteLine("Creating two accounts locally");
        Console.WriteLine(account1);
        Console.WriteLine(account2);

        Console.WriteLine( );
        Console.WriteLine("POSTing accounts to server"  );
        account1 = consumer.AddAccount(account1);
        account2 = consumer.AddAccount(account2);

        Console.WriteLine();

        Console.WriteLine("Verifying the accounts were added by inspecting their server generated IDs");
        Console.WriteLine(account1);
        Console.WriteLine(account2);

        Console.WriteLine();
        Console.WriteLine("Retrieving all accounts from server:");

        var accounts = consumer.GetAllAccounts();
        foreach (var account in accounts)
        {
            Console.WriteLine($"Account: {account}");
        }
        Console.WriteLine();
        Console.WriteLine($"Deleting account with ID {account1.Id}");
        consumer.DeleteAccount(account1.Id);

        Console.WriteLine();
        Console.WriteLine("Showing remaining accounts:");
        accounts = consumer.GetAllAccounts();
        foreach (var account in accounts)
        {
            Console.WriteLine($"Account: {account}");
        }

        Console.WriteLine("ENTER to exit");

        Console.ReadLine();
    }
}
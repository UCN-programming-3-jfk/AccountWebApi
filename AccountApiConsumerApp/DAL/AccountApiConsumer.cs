using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AccountApiConsumerApp.DAL
{
    public class AccountApiConsumer : IAccountApiConsumer
    {
        public string ServiceUri { get; private set; }
        public AccountApiConsumer(string baseServiceUri)
        {
            ServiceUri = baseServiceUri;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            //create a client for calling the server
            var client = new RestClient(ServiceUri);

            //execute a RestRequest (default is GET)
            //and return a List<Account>,
            //which is automatically deserialized to objects
            var response = client.Execute<List<Account>>(new RestRequest());

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all accounts. Message was {response.StatusDescription}");
            }
            return response.Data;
        }

        public Account AddAccount(Account accountToAdd)
        {

            //create a new request, using POST, with the JSON from the account
            var request = new RestRequest().AddJsonBody(accountToAdd);
            //call the server
            var client = new RestClient(ServiceUri);
            //get a response
            var response = client.ExecutePost<Account>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error adding account. Message was {response.StatusDescription}");
            }
            return response.Data;
        }

        public void DeleteAccount(int idOfAccountToDelete)
        {
            //call the server
            var client = new RestClient($"{ServiceUri}/{idOfAccountToDelete}");
            //get a response
            var response = client.Delete(new RestRequest());

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error deleting account with id {idOfAccountToDelete}. Message was {response.StatusDescription}");
            }

        }
    }
}
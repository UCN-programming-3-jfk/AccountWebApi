using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
namespace AccountApiConsumerApp.DAL;
public class AccountApiConsumer : IAccountApiConsumer
{
    private string _serviceUri;
    private RestClient _restClient;

    public AccountApiConsumer(string baseServiceUri)
    {
        _serviceUri = baseServiceUri;
        //create a client for calling the server
        _restClient = new RestClient(_serviceUri);
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        //execute a RestRequest (default is GET)
        var response = _restClient.Execute<List<Account>>(new RestRequest());

        //give an error if the response is not successful or the data is null
        if (!response.IsSuccessful || response.Data == null)
        {
            throw new Exception($"Error retrieving all accounts. Message was {response.StatusDescription}");
        }

        //and return a List<Account>,
        //which is automatically deserialized to objects
        return response.Data;
    }

    public Account AddAccount(Account accountToAdd)
    {
        //create a new request, using POST, with the JSON from the account
        var request = new RestRequest().AddJsonBody(accountToAdd);
        //get a response
        var response = _restClient.ExecutePost<Account>(request);

        if (!response.IsSuccessful || response.Data == null)
        {
            throw new Exception($"Error adding account. Message was {response.StatusDescription}");
        }
        return response.Data;
    }

    public void DeleteAccount(int idOfAccountToDelete)
    {
        //get a response
        var response = _restClient.ExecuteDelete<bool>(new RestRequest($"{idOfAccountToDelete}"));

        //give an error if the response is not successful
        if (!response.IsSuccessful)
        {
            throw new Exception($"Error deleting account with id {idOfAccountToDelete}. " 
               + "Message was {response.StatusDescription}");
        }
    }
}
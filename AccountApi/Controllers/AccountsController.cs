using AccountApi.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    #region Variables and constructor
    private IAccountDAO _accountDAO;
    public AccountsController(IAccountDAO dataAccessLayer)
    {
        _accountDAO = dataAccessLayer;
    } 
    #endregion

    #region RESTful CRUD methods
    [HttpGet]
    public ActionResult<IEnumerable<Account>> GetAll()
    {
        //returns 200 + account JSON as body
        return Ok(_accountDAO.GetAll());
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Account> GetAccountUsingId(int id)
    {
        Account? account = _accountDAO.Get(id);
        if (account == null)
        {
            return NotFound();  //returns 404
        }
        return Ok(account); //returns 200 + account JSON as body
    }

    [HttpPost]
    public ActionResult<Account> AddAccount(Account account)
    {
        account.Id = _accountDAO.Insert(account);

        //returns 201 + account JSON as body
        return Created($"{baseURI}/{account.Id}", account); 
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult DeleteAccount(int id)
    {
        if (!_accountDAO.Delete(id))
        {
            return NotFound();  //returns 404
        }   
        return Ok();    //returns 200
    }

    [HttpPut]
    public ActionResult UpdateAccount(Account account)
    {
        if (!_accountDAO.Update(account))
        {
            return NotFound();  //returns 404
        }
        return Ok();    //returns 200
    }
    #endregion
}
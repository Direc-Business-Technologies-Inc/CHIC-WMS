using DataManager.Libraries.Repositories;
using DataManager.Models.APIs;
using DataManager.Models.Users;
using DataManager.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DataManager.ApiServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    readonly IAuthDataService _authDataService;
    readonly IMySqlDataAccess _mySql;
    public LoginController(IAuthDataService authDataService, IMySqlDataAccess mySql)
    {
        _authDataService = authDataService; 
        _mySql = mySql;
    }

    [HttpPost]
    public async Task<HttpResponseMessage> Post(Credentials login)
    {
        UserLogins user = new()
        {
            Username = login.Username,
            Password = login.Password
        };

        return await Task.FromResult(_authDataService.Login(user));

    }

    //[HttpGet]
    //public async Task<List<UserLogins>> Get(string username, string password)
    //{
    //    return await _mySql.GetData<UserLogins, dynamic>(
    //        $"SELECT * " +
    //        $"FROM USRL " +
    //        $"WHERE Username = @Username " +
    //        $"AND Password = @Password"
    //        , new { Username = username, Password = password }, System.Data.CommandType.Text);
    //}
}

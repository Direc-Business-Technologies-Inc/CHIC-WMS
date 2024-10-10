using DataManager.Libraries.Repositories;
using DataManager.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataManager.ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        IMySqlDataAccess _mySql;

        public TestsController(IMySqlDataAccess mySql)
        {
            _mySql = mySql;
        }

        // GET: api/<TestsController>
        [HttpGet]
        public async Task<List<UserLogins>> Get()
        {
            await Task.Delay(1000);
            return _mySql.GetData<UserLogins, dynamic>($"SELECT UserId FROM USRL WHERE Username = @Username", new { Username = "admin" }, System.Data.CommandType.Text);

        }

        // GET api/<TestsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestsController>
        [HttpPost]
        public async Task<int> Post([FromBody] string value)
        {
            return await _mySql.Execute("UPDATE USRL SET UserId = UserId", new { }, System.Data.CommandType.Text);
        }

        // PUT api/<TestsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

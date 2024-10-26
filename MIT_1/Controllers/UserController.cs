using Microsoft.AspNetCore.Mvc;
using MIT_1.Data;
using MIT_1.Model;

namespace MIT_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UserController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet, Route("{id}")]
        public ActionResult Get(int id)
        {
            var user = _db.Users
                .Select(x => new User
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Description = x.Description,
                    Limit = x.Limit,
                })
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var users = _db.Users
                .Select(x => new User
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Description = x.Description,
                    Limit = x.Limit,
                })
                .ToList();

            return Ok(users);
        }

        [HttpPost]
        public ActionResult Post(User user)
        {
            var error = ValidateUser(user);
            if (error != null)
                return BadRequest(error);

            _db.Users.Add(user);
            _db.SaveChanges();

            return Created("", user);
        }

        [HttpPut]
        public ActionResult Put(User user)
        {
            var error = ValidateUser(user);
            if (error != null)
                return BadRequest(error);

            _db.Users.Update(user);
            _db.SaveChanges();

            return Ok(user);
        }

        [HttpDelete, Route("{id}")]
        public ActionResult Delete(int id)
        {
            var user = _db.Users.Find(id);

            if (user == null)
                return NotFound();

            _db.Users.Remove(user);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost, Route("login")]
        public ActionResult Login(LoginDto login)
        {
            var user = _db.Users
                .Select(x => new User
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Description = x.Description,
                    Limit = x.Limit,
                })
                .FirstOrDefault(x => x.Email == login.Email);

            if (user != null && user.Password == login.Password)
            {
                return Ok(user);
            }

            HttpContext.Response.StatusCode = 403;
            return Empty;
        }

        private string? ValidateUser(User user)
        {
            if (user.Agree != true)
                return "You must agree with terms and conditions";

            if (user.Limit != "600" && user.Limit != "756" && user.Limit != "1000" && user.Limit != "2000" && user.Limit != "PURE" && user.Limit != "FLAT")
                return "Invalid Limit";

            return null;
        }
    }
}

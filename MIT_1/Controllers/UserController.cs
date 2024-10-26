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
            var user = _db.Users.Find(id);
            
            return Ok(user);
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            var users = _db.Users.ToList();
            
            return Ok(users);
        }
        
        [HttpPost]
        public ActionResult Post(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            
            return Created("", user);
        }
        
        [HttpPut]
        public ActionResult Put(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
            
            return Created("", user);
        }
    }
}

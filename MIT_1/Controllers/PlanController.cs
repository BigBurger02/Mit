using Microsoft.AspNetCore.Mvc;
using MIT_1.Data;
using MIT_1.Model;

namespace MIT_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PlanController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet, Route("{id}")]
        public ActionResult Get(int id)
        {
            var plan = _db.Plans.Find(id);
            
            return Ok(plan);
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            var plans = _db.Plans.ToList();
            
            return Ok(plans);
        }
        
        [HttpPost]
        public ActionResult Post(Plan plan)
        {
            _db.Plans.Add(plan);
            _db.SaveChanges();
            
            return Created("", plan);
        }
        
        [HttpPut]
        public ActionResult Put(Plan plan)
        {
            _db.Plans.Update(plan);
            _db.SaveChanges();
            
            return Created("", plan);
        }
    }
}

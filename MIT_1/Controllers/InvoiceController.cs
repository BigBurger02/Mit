using Microsoft.AspNetCore.Mvc;
using MIT_1.Data;
using MIT_1.Model;

namespace MIT_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _db;

        public InvoiceController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet, Route("{id}")]
        public ActionResult Get(int id)
        {
            var invoice = _db.Invoices.Find(id);
            
            return Ok(invoice);
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            var invoices = _db.Invoices.ToList();
            
            return Ok(invoices);
        }
        
        [HttpPost]
        public ActionResult Post(Invoice invoice)
        {
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
            
            return Created("", invoice);
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

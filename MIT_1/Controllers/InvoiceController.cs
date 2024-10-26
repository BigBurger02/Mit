using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult Post(Invoice newInvoice)
        {
            var invoice = new Invoice
            {
                UserId = newInvoice.UserId,
                InTraffic = newInvoice.InTraffic,
                OutTraffic = newInvoice.OutTraffic,
                StartDate = newInvoice.StartDate,
                EndDate = newInvoice.EndDate,
            };

            var user = _db.Users
                .Find(newInvoice.UserId);

            invoice.Limit = user.Limit;

            CalculateCost(invoice);
            
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
            
            return Created("", invoice);
        }
        
        [HttpPut]
        public ActionResult Put(Invoice updateInvoice)
        {
            var invoice = _db.Invoices.Find(updateInvoice.Id);

            invoice.InTraffic = updateInvoice.InTraffic;
            invoice.OutTraffic = updateInvoice.OutTraffic;
            invoice.StartDate = updateInvoice.StartDate;
            invoice.EndDate = updateInvoice.EndDate;
            
            CalculateCost(invoice);
            
            _db.Invoices.Update(invoice);
            _db.SaveChanges();
            
            return Created("", invoice);
        }

        private void CalculateCost(Invoice invoice)
        {
            if (invoice.User == null)
            {
                throw new ArgumentNullException();
            }
            
            if (invoice.User.Limit == "600")
            {
                if (invoice.InTraffic < 600)
                    invoice.Cost = 30;
                else
                    invoice.Cost = invoice.InTraffic / 1000 * 50;
            }
            if (invoice.User.Limit == "756")
            {
                if (invoice.InTraffic < 750)
                    invoice.Cost = 73;
                else if (invoice.InTraffic < 1000)
                    invoice.Cost = 55;
                else if (invoice.InTraffic < 2000)
                    invoice.Cost = 55 * invoice.InTraffic / 1000;
                else
                    invoice.Cost = invoice.InTraffic / 1000 * 60;
            }
            if (invoice.User.Limit == "1000" || invoice.User.Limit == "2000")
            {
                if (invoice.InTraffic < 1000)
                    invoice.Cost = 55;
                else if (invoice.InTraffic < 2000)
                    invoice.Cost = (invoice.InTraffic + invoice.OutTraffic) / 1000 * 55;
                else if (invoice.InTraffic < 10000)
                    invoice.Cost = (invoice.InTraffic + invoice.OutTraffic) / 1000 * 60;
                else
                    invoice.Cost = (invoice.InTraffic + invoice.OutTraffic) / 1000 * 53 * (110 - (invoice.InTraffic + invoice.OutTraffic) / 1000) / 100;
            }
            if (invoice.User.Limit == "PURE")
            {
                invoice.Cost = invoice.InTraffic / 1000 * 85;
            }
            if (invoice.User.Limit == "FLAT")
            {
                invoice.Cost = invoice.InTraffic;
            }
        }
    }
}

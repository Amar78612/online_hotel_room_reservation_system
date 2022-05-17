using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineHotelRoomReservationSystem.Models;
using OnlineHotelRoomReservationSystem.Repository;
using System;

namespace OnlineHotelRoomReservationSystem.Controllers
{
    
        public class CustomersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();    
        public IQueryable<Customer> GetCustomers()
        {
            return db.Customers;
        }

        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var key = "b14ca5898a4e4133bbce2ea2315a1916";
                var encryptedPassword = AesOperation.EncryptString(key, customer.Password);
                Customer C = new Customer();
                C.FirstName = customer.FirstName;
                C.LastName = customer.LastName;
                C.AadharNumber = customer.AadharNumber;
                C.PhoneNumber = customer.PhoneNumber;
                C.EmailId = customer.EmailId;
                C.Password = encryptedPassword;
                C.Gender = customer.Gender;
                db.Customers.Add(C);
                db.SaveChanges();
                return Ok();
            }
        }

        [Route("api/customerauth")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IHttpActionResult response = BadRequest("Invalid email or password");
            var key = "b14ca5898a4e4133bbce2ea2315a1916";
            var encryptedPassword = AesOperation.EncryptString(key, model.Password);
            using (var context = new ApplicationDbContext())
            {
                if (context.Customers.Any(u => u.EmailId == model.EmailId && u.Password == encryptedPassword))
                {
                    var result = (from user in context.Customers
                                  where user.EmailId == model.EmailId
                                  select new { user.CustomerId, user.EmailId }).Single();
                    response = Ok(result);
                }
            }
            return response;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
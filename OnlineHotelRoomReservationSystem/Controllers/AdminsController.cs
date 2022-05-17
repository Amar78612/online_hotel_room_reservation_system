using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineHotelRoomReservationSystem.Models;
using OnlineHotelRoomReservationSystem.Repository;

namespace OnlineHotelRoomReservationSystem.Controllers
{
    public class AesOperation
    {
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }
    }
    public class AdminsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IQueryable<Admin> GetAdmins()
        {
            return db.Admins;
        }

        [ResponseType(typeof(Admin))]
        public IHttpActionResult GetAdmin(int id)
        {
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }
        [Route("api/adminauth")]
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
                if (context.Admins.Any(u => u.EmailId == model.EmailId && u.Password == encryptedPassword))
                {
                    var result = (from user in context.Admins
                                  where user.EmailId == model.EmailId
                                  select new { user.AdminId, user.EmailId }).Single();
                    response = Ok(result);
                }
            }
            return response;
        }

        [ResponseType(typeof(Admin))]
        public IHttpActionResult PostAdmin(Admin admin)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var key = "b14ca5898a4e4133bbce2ea2315a1916";
                var encryptedPassword = AesOperation.EncryptString(key, admin.Password);
  
                Admin A = new Admin();
                A.FirstName = admin.FirstName;
                A.LastName = admin.LastName;
                A.EmailId = admin.EmailId;
                A.Password = encryptedPassword;
                db.Admins.Add(A);
                db.SaveChanges();
                return Ok();
            }
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
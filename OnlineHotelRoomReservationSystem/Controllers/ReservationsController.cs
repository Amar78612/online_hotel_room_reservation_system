using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineHotelRoomReservationSystem.Models;

namespace OnlineHotelRoomReservationSystem.Controllers
{
    public class ReservationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult GetReservation(int id)
        {
            var result = from reservations in db.Reservations
                         where reservations.CustomerId == id
                         select new
                         {
                            reservations.ReservationId,
                            reservations.CustomerId,
                            reservations.CheckIn,
                            reservations.CheckOut,
                            reservations.RoomId, 
                         };                                                                                    

            return Ok(result);
        }

        [ResponseType(typeof(Reservation))]
        public IHttpActionResult PostReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reservations.Add(reservation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reservation.ReservationId }, reservation);
        }
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservation);
            db.SaveChanges();

            return Ok(reservation);
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

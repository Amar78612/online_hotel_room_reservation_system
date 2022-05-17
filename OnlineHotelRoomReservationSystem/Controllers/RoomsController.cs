using AutoMapper;
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
using OnlineHotelRoomReservationSystem.Repository;

namespace OnlineHotelRoomReservationSystem.Controllers
{
    public class RoomsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var result = from rooms in db.Rooms
                         select new
                         {
                             rooms.RoomId,
                             rooms.RoomNumber,
                             rooms.FloorNumber,
                             rooms.CostPerDay,
                             rooms.RoomCapacity,
                             rooms.RoomStatus,
                             rooms.RoomType,
                             Hotel = from hotels in db.Hotels
                                     where(hotels.HotelId == rooms.HotelId)
                                     select new
                                     {
                                         hotels.HotelId,
                                         hotels.HotelName
                                     }
                         };
          
            return Ok(result.ToList());
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, [FromBody] Room room)
        {
            
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var entity = db.Rooms.FirstOrDefault(r => r.RoomId == id);
                    if (entity == null)
                    {
                        return Content(HttpStatusCode.NotFound, "Room with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.RoomNumber = room.RoomNumber;
                        entity.FloorNumber = room.FloorNumber;
                        entity.RoomType = room.RoomType;
                        entity.RoomCapacity = room.RoomCapacity;
                        entity.CostPerDay = room.CostPerDay;
                        entity.RoomStatus = room.RoomStatus;

                        db.SaveChanges();

                        return Content(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = room.RoomId }, room);
        }

        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
            db.SaveChanges();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.RoomId == id) > 0;
        }
    }
}
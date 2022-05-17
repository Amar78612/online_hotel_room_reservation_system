using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineHotelRoomReservationSystem.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        public int FloorNumber { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public string RoomType { get; set; }
        [Required]
        public int RoomCapacity { get; set; }
        [Required]
        public int CostPerDay { get; set; }
        [Required]
        public string RoomStatus { get; set; }
        [Required]
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        
    }
}
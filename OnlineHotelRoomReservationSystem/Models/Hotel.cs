using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineHotelRoomReservationSystem.Models
{
    [Table("Hotels")]
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        [Required]
        [MaxLength(100)]
        public string HotelName { get; set; }
        [Required]
        public string HotelAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [MaxLength(6)]
        [RegularExpression("[0-9]+")]
        public string PinCode { get; set; }
        [Required]
        public string IsActive { get; set; }

    }
}
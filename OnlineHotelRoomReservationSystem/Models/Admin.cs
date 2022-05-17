using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OnlineHotelRoomReservationSystem.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        [RegularExpression(@"[a-z A-Z]+")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"[a-z A-Z]+")]
        public string LastName { get; set; }
        [Required]
        [Index("IX_EmailId", IsUnique = true)]
        [MaxLength(100)]
        public string EmailId { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
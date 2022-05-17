using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineHotelRoomReservationSystem.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [RegularExpression(@"[a-z A-Z]+")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"[a-z A-Z]+")]
        public string LastName { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 12)]
        [RegularExpression("[0-9]+")]
        [Index("IX_AadharNumber", IsUnique =true)]
        public string AadharNumber { get; set; }
        [Required]
        [StringLength(10,MinimumLength =10)]
        [RegularExpression("[0-9]+")]
        [Index("IX_PhoneNumber", IsUnique = true)]
        public string PhoneNumber { get; set; }
        [Required]
        [Index("IX_EmailId", IsUnique = true)]
        [MaxLength(100)]
        public string EmailId { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
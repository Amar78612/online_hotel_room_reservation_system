using System.ComponentModel.DataAnnotations;

namespace OnlineHotelRoomReservationSystem.Repository
{
    public class LoginRequest
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
using Core.Entities.Rooms;
using Microsoft.AspNetCore.Identity;

namespace Core.User
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}

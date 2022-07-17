using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        // Primary key
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Registered { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests {get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        // One to many relationship i.e one user can have multible photos
        public ICollection<Photo> Photos { get; set; }
        public ICollection<AppUserLike> LikedByUsers { get; set; }
        public ICollection<AppUserLike> LikedUsers { get; set; }

    }
}
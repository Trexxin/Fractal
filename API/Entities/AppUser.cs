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
        public string LookingFor { get; set; }
        public string Interests {get; set; }
        public string City { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
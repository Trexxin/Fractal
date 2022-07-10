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
        public string LookingFor { get; set; }
        public string Interests {get; set; }
        public string City { get; set; }
        // One to many relationship i.e one user can have multible photos
        public ICollection<Photo> Photos { get; set; }
        // Returns age of the user based on their date of birth from an extension method
        // Will populate the variable "Age" inside of the MemberDto because of AutoMapper
        // public int GetAge()
        // {
        //     return DateOfBirth.CalculateAge();
        // }
    }
}
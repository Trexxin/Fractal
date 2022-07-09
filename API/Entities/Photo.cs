using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    // Names the tables Photos in the database (Just for better naming conventinon)
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool isMain { get; set; }
        public string PulbicId { get; set; }
        // Fully defining the relationship with AppUser
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}
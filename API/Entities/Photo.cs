namespace API.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool isMain { get; set; }
        public string PulbicId { get; set; }
    }
}
namespace Noted.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[] Data { get; set; } = null!;
        public string ContentType { get; set; } = null!;
    }
}

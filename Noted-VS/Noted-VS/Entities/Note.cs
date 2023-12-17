namespace Noted.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Data { get; set; }
        public Repository Repository { get; set; }
        public int RepositoryId { get; set; }
    }
}

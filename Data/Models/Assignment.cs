namespace Data.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public ulong OwnerId { get; set; }
    }
}

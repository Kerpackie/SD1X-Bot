namespace Data.Models
{
    public class CSharpTag
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Content { get; set; }
        public ulong OwnerId { get; set; }
    }
}

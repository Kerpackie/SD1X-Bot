namespace Data.Models
{
    public class CSSTag
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Content { get; set; }
        public ulong OwnerId { get; set; }
    }
}

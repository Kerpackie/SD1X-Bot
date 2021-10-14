
namespace Data.Models
{
    public class TimeTable
    {
        public int Id { get; set; }
        public ulong GuildId { get; set; }
        public string Day { get; set; }
        public string Subject { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
    }
}

namespace CommunityConnect.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateOnly date {  get; set; }
        public string Description { get; set; }
    }
}

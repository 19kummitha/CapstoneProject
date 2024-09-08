namespace CommunityConnect.Models
{
    public class Complaint
    {
        public long ComplaintId { get; set; }
        public string PersonName { get; set; }
        public string FlatNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ComplaintStatus Status { get; set; } = ComplaintStatus.OPEN;
        public string ResidentId { get; set; }  
    }

    public enum ComplaintStatus
    {
        OPEN=1,
        CLOSED=0
    }
}

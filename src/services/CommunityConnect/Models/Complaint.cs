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
        public long ResidentId { get; set; }
        public Resident Resident { get; set; }
    }
    public class Resident
    {
        public long ResidentId { get; set; }
        public string Name { get; set; }
        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
    }
    public enum ComplaintStatus
    {
        OPEN,
        CLOSED
    }
}

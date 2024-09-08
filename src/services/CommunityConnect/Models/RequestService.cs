namespace CommunityConnect.Models
{
    public class RequestService
    {
        public int RequestServiceId {  get; set; }
        public string ResidentName {  get; set; }
        public string ServiceType {  get; set; }
        public string ServiceDescription { get; set; }
        public string FlatNo {  get; set; }
        public string Status {  get; set; }
        public string? ResidentId {  get; set; }
    }
}

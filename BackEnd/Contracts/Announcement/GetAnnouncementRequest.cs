namespace BackEnd.Contracts.Announcement
{
    public class GetAnnouncementRequest
    {
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int CategoryId { get; set; } = 0;
        public string? SearchString { get; set; }
    }
}

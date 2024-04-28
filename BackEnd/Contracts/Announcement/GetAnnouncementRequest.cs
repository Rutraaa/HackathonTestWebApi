namespace BackEnd.Contracts.Announcement
{
    public class GetAnnouncementRequest
    {
        public int currentPage { get; set; }
        public int pageSize { get; set; }
    }
}

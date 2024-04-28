using BackEnd.DTO;

namespace BackEnd.Contracts.Announcement
{
    public class GetAnnouncementResponse
    {
        public List<AnnouncementDTO> items { get; set; }
        public int totalCount { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}

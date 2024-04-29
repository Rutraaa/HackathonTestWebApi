using BackApi.DataTypes;

namespace BackEnd.Contracts.Announcement
{
    public class CreateAnnouncementRequest
    {
        public int ConsumerId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public AnnouncementStatus Status { get; set; }
    }
}

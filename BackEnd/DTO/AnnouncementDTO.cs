namespace BackEnd.DTO
{
    public class AnnouncementDTO
    {
        public int Id { get; set; }
        public int ConsumerId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
    }
}

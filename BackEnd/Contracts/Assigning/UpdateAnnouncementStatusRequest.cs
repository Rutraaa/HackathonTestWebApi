namespace BackEnd.Contracts.Assigning;

public class UpdateAnnouncementStatusRequest
{
    public int Status { get; set; }
    public int AnnouncementId { get; set; }
}
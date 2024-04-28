namespace BackEnd.Contracts.FileHolder;

public class UploadFilesRequest
{
    public int AnnouncementId { get; set; }
    public List<string> FileNameList { get; set; }
}
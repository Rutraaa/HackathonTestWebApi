namespace BackEnd.Contracts.Consumer;

public class AccessInfoResponse
{
    public string AccessToken { get; set; }
    public int UserId { get; set; }
    public bool IsSupllier { get; set; }
}
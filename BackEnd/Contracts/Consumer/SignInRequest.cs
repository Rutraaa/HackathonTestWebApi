namespace BackEnd.Contracts.Consumer;

public class SignInRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsSupllier { get; set; }
}
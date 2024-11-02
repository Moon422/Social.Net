namespace Social.Net.Api.Models.Users;

public record LoginModel : BaseModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
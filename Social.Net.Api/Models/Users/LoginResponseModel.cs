namespace Social.Net.Api.Models.Users;

public record LoginResponseModel : BaseModel
{
    public string JwtToken { get; set; }
    
    public ProfileModel? Profile { get; set; }
}
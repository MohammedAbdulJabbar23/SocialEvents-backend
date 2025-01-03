namespace API.DTOs;


public class UserDto
{
    public required string DisplayName { get; set; }
    public string Token { get; set; }
    public string? Image { get; set; }
    public required string Username { get; set; }

}
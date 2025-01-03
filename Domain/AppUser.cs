using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    public required string DisplayName { get; set; }
    public string? Bio { get; set; }
}
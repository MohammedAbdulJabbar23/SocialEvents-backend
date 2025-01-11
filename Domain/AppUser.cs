using Domain;
using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    public required string DisplayName { get; set; }
    public string? Bio { get; set; }

    public ICollection<ActivityAttendee> Activities { get; set; } = new List<ActivityAttendee>();
}
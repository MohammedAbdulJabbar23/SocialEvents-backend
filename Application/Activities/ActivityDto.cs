using Application.Profiles;

namespace Application.Activities;


public class ActivityDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public required string City { get; set; }
    public required string Venue { get; set; }
    public required string HostUsername { get; set; }
    public bool IsCancelled { get; set; }
    public ICollection<Profile> Attendees { get; set; } = new List<Profile>();   
}
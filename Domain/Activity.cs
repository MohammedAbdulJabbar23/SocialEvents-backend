
namespace Domain;

public class Activity
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public required string City { get; set; }
    public required string Venue { get; set; }
}
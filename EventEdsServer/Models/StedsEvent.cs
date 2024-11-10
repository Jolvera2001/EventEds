using System.ComponentModel.DataAnnotations;

namespace EventEdsServer.Models;

public class StedsEvent
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; } 
    
    // relationship
    public Location Location { get; set; }
    public Guid LocationId { get; set; }
}

public class StedsEventDto
{
    public DateTime CreatedOn { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; } 
    public LocationDto Location { get; set; }
}
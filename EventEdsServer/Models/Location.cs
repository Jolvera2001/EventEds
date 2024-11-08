namespace EventEdsServer.Models;

public class Location
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }
    
    // relationship
    public ICollection<StedsEvent> Events { get; set; }
}

public class LocationDto
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Name { get; set; }
}
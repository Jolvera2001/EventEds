using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace EventEdsServer.Models;

public class StedsEvent
{
    [BsonId]
    public ObjectId Id { get; set; }
    
    [BsonElement("created_on")]
    public DateTime CreatedOn { get; set; }
    
    [BsonElement("title")]
    public string Title { get; set; }
    
    [BsonElement("description")]
    public string Description { get; set; }
    
    [BsonElement("start_date")]
    public DateTime StartDate { get; set; }
    
    [BsonElement("end_date")]
    public DateTime EndDate { get; set; } 
    
    [BsonElement("location")]
    public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
}

public class StedsEventDto
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }

    [Required] 
    public LocationDto Location { get; set; } = null!;

    public GeoJsonPoint<GeoJson2DGeographicCoordinates> ToLocation()
    {
        return new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
            new GeoJson2DGeographicCoordinates(
                Location.Coordinates[0],
                Location.Coordinates[1]
                )
            );
    }
}

public class LocationDto
{
    public string Type { get; set; } = "Point";
    public double[] Coordinates { get; set; } = null!;
}
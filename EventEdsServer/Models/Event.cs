using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace EventEdsServer.Models;

public class Event
{
    [BsonId]
    public ObjectId Id { get; set; }
    
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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EventEds.Client.Components;

public partial class CreateEventCard : ComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; }

    private IJSObjectReference? _mapModule;
    private DotNetObjectReference<CreateEventCard>? _objectReference;
    private const string MapBoxToken = "";
    
    public class FormModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime EventDay { get; set; } = DateTime.Today;
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public Location Location { get; set; }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    
    private FormModel? _formModel = new FormModel();
    private Location? _location = new Location();

    protected override async void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _mapModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/mapboxInterop.js");
            _objectReference = DotNetObjectReference.Create(this);

            await InitializeMap(51.509865, -0.118092);
        }
        
    }

    private async Task InitializeMap(double latitude, double longitude)
    {
        if (_mapModule != null)
        {
            await _mapModule.InvokeVoidAsync("initializeMap", "map", MapBoxToken, latitude, longitude);
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace AdminWatchServer.Models;

public class DeviceMemoryOccupy
{
    public int Id { get; set; }

    [Required]
    public required double OccupiedMemory { get; set; }

    [Required]
    public required double TotalMemory { get; set; }

    [Required]
    public required DateTime MeasureTime { get; set; }
}
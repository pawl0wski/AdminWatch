using System.ComponentModel.DataAnnotations;

namespace AdminWatchServer.Models;

public class DeviceCpuUtilization
{
    public int Id { get; set; }

    [Required]
    [Range(0, 100)]
    public int Utilization { get; set; }

    [Required]
    public DateTime MeasureTime { get; set; }
}
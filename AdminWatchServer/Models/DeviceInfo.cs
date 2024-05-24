using System.ComponentModel.DataAnnotations;

namespace AdminWatchServer.Models;

public class DeviceInfo
{
    [Required] public int Id { get; set; }

    [MaxLength(64)]
    public string Os { get; set; } = "";

    [MaxLength(15)]
    public string Ip { get; set; } = "";

    [MaxLength(128)]
    public string ProcessorName { get; set; } = "";

    public double TotalMemory { get; set; } = 0;

    [MaxLength(128)] 
    public string Manufacturer { get; set; } = "";

    [MaxLength(128)] 
    public int Battery { get; set; } = 0;
}
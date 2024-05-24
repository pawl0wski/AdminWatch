using System.ComponentModel.DataAnnotations;

namespace AdminWatchServer.Models;

public class DeviceInfo
{
    [Required]
    public int Id { get; set; }

    [MaxLength(64)] 
    public string Os { get; set; } = "";

    [MaxLength(15)] 
    public string Ip { get; set; } = "";

    [MaxLength(255)]
    public string ProcessorName { get; set; } = "";

    public double TotalMemory { get; set; } = 0;

}
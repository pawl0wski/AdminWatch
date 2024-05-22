
using System.ComponentModel.DataAnnotations;

namespace AdminWatchServer.Models;

public class Device
{
    public required int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required DeviceOs Os { get; set; }

    public enum DeviceOs
    {
        Windows,
        Linux
    }
}
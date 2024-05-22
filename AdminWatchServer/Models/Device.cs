
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminWatchServer.Models;

public class Device
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }

    [DefaultValue(DeviceStatus.Disconnected)]
    public DeviceStatus Status { get; set; }
    
    [Required]
    public required DeviceInfo Info { get; set; }

    public enum DeviceStatus
    {
        Disconnected,
        Connected
    }
}
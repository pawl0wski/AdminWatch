
using System.ComponentModel.DataAnnotations;

namespace AdminWatchServer.Models;

public class Device
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }

    [Required] 
    public required DeviceInfo Info { get; set; }
}
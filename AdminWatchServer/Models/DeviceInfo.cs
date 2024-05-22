using System.ComponentModel.DataAnnotations;

namespace AdminWatchServer.Models;

public class DeviceInfo
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required DeviceOs Os { get; set; }

    [Required]
    [MaxLength(15)]
    public required string Ip { get; set; }

    [Required]
    [MaxLength(11)] 
    public required string MacAdress { get; set; }

    [Required]
    [MaxLength(255)]
    public required string ProcessorName { get; set; }
    

    public enum DeviceOs
    {
        Windows,
        Linux
    }
}
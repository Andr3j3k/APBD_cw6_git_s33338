using System.ComponentModel.DataAnnotations;

namespace APBD_cw6_git_s33338.DTOs;

public class UpdateRoomDto
{
    [Required]
    public string Name { get; set; } = String.Empty;

    [Required]
    public string BuildingCode { get; set; } = String.Empty;

    public int Floor { get; set; }
    
    public int Capacity { get; set; }

    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}
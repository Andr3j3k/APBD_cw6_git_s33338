using System.ComponentModel.DataAnnotations;

namespace APBD_cw6_git_s33338.DTOs;

public class UpdateReservationDto
{
    [Required]
    public int RoomId { get; set; }

    [Required]
    public string OrganizerName { get; set; } = String.Empty;

    [Required]
    public string Topic { get; set; } = String.Empty;

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }

    [Required]
    public string Status { get; set; } = String.Empty;
}
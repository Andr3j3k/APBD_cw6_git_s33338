namespace APBD_cw6_git_s33338.Models;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId{ get; set; }
    public string OrganizerName{ get; set; }=String.Empty;
    public string Topic{ get; set; }=String.Empty;
    public DateOnly Date{ get; set; }
    public TimeOnly StartTime{ get; set; }
    public TimeOnly EndTime{ get; set; }
    public string Status{ get; set; }=String.Empty;
}
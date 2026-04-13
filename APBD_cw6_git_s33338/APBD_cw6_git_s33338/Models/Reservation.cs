namespace APBD_cw6_git_s33338.Models;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId{ get; set; }
    public string OrganizerName{ get; set; }=String.Empty;
    public string Topic{ get; set; }=String.Empty;
    public string Date{ get; set; }=String.Empty;
    public string StartTime{ get; set; }=String.Empty;
    public string EndTime{ get; set; }=String.Empty;
    public string Status{ get; set; }=String.Empty;
}
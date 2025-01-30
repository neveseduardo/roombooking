namespace RoomBooking.Models.ViewModels;
public class UserViewModel
{
    public int id { get; init; }
    public string name { get; set; } = "";
    public string email { get; set; } = "";
    public string[] roles { get; set; } = [];
}


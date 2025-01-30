namespace RoomBooking.Models.ViewModels;

public class RoomViewModel
{
    public int id { get; set; }
    public string nome { get; set; } = "";
    public string descricao { get; set; } = "";
    public string capacidade { get; set; } = "";
    public string categoria { get; set; } = "";
    public string status { get; set; } = "";
}

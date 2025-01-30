namespace RoomBooking.Models.ViewModels;

public class BookingViewModel
{
    public int id { get; set; }
    public string hora_inicio { get; set; } = "";
    public string hora_fim { get; set; } = "";
    public string data_reserva { get; set; } = "";
    public string? protocolo { get; set; } = "";
    public string status { get; set; } = "";
    public int sala_id { get; set; }
    public int cliente_id { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Models.Dto;

public class CreateBookingDto
{
    [Required(ErrorMessage = "Preenchimento do Campo [hora inicial] Obrigatório!")]
    public string hora_inicio { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo [hora final] Obrigatório!")]
    public string hora_fim { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo [data da reserva] Obrigatório!")]
    public string data_reserva { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo [sala] Obrigatório!")]
    public int sala_id { get; set; }

    [Required(ErrorMessage = "Preenchimento do Campo [cliente] Obrigatório!")]
    public int cliente_id { get; set; }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBooking.Models;

[Table("reservas")]
public class Booking
{
    [Key]
    public int id { get; set; }

    [Required(ErrorMessage = "O campo [hora inicial] é obrigatório.")]
    public string hora_inicio { get; set; } = "";

    [Required(ErrorMessage = "O campo [hora final] é obrigatório.")]
    public string hora_fim { get; set; } = "";

    [Required(ErrorMessage = "O campo [data da reserva] é obrigatório.")]
    public string data_reserva { get; set; } = "";


    [Required(ErrorMessage = "O campo [status] é obrigatório.")]
    public string status { get; set; } = "Pendente";
    public string? protocolo { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

    [Required(ErrorMessage = "O campo [sala] é obrigatório.")]
    [ForeignKey("room")]
    public int sala_id { get; set; }

    [Required(ErrorMessage = "O campo [cliente] é obrigatório.")]
    [ForeignKey("customer")]
    public int cliente_id { get; set; }

    public Customer? customer { get; set; }
    public Room? room { get; set; }

    public string? created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    public string? updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
}
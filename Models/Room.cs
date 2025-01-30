using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBooking.Models;

[Table("salas")]
public class Room
{
    [Key]
    public int id { get; set; }

    [Required(ErrorMessage = "Preenchimento do Campo 'nome' Obrigatório!")]
    public string nome { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo 'descricao' Obrigatório!")]
    public string descricao { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo 'capacidade' Obrigatório!")]
    public string capacidade { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo 'categoria' Obrigatório!")]
    public string categoria { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo 'status' Obrigatório!")]
    public string status { get; set; } = "";

    public string? created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";

    public string? updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
}

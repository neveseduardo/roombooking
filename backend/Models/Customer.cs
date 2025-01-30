using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBooking.Models;

[Table("clientes")]
public class Customer
{
    [Key]
    public int id { get; set; }

    [Required(ErrorMessage = "Preenchimento do Campo 'nome' Obrigatório!")]
    public string nome { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo 'email' Obrigatório!")]
    public string email { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo 'cpf' Obrigatório!")]
    public string cpf { get; set; } = "";

    public string? created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";

    public string? updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
}

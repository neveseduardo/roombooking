using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBooking.Models;

[Table("users")]
public class User
{
    [Key]
    public int id { get; init; }

    [Required(ErrorMessage = "Preenchimento do Campo 'nome' Obrigatório!")]
    [Display(Name = "Nome")]
    public string name { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo 'email' Obrigatório!")]
    [Display(Name = "E-mail")]
    public string email { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo 'senha' Obrigatório!")]
    [Display(Name = "Senha")]
    public string password { get; set; } = "";
    public string[] roles { get; set; } = [];
    public string created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    public string updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
}
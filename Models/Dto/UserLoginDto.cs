using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBooking.Models.Dto;
public class UserLoginDto
{
    [Required(ErrorMessage = "Preenchimento do Campo 'e-mail' Obrigatório!")]
    public string email { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo 'senha' Obrigatório!")]
    public string password { get; set; } = "";
}
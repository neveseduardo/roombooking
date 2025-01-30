using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Models.Dto;

public class UpdateCustomerDto
{
    [Required(ErrorMessage = "Preenchimento do Campo 'nome' Obrigatório!")]
    public string nome { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo 'email' Obrigatório!")]
    [EmailAddress(ErrorMessage = "O email não é válido.")]
    public string email { get; set; } = "";

    [Required(ErrorMessage = "Preenchimento do Campo 'cpf' Obrigatório!")]
    public string cpf { get; set; } = "";
}

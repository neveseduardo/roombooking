using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Models.Dto;

public class CreateRoomDto
{
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
}

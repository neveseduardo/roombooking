using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoomBooking.Models;
using RoomBooking.Models.Dto;
using RoomBooking.Models.ViewModels;
using RoomBooking.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomBooking.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILogger<RoomsController> _logger;

    public RoomsController(IRoomRepository roomRepository, ILogger<RoomsController> logger)
    {
        _roomRepository = roomRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomViewModel>>> GetRooms()
    {
        _logger.LogInformation("Obter todas as salas.");

        var rooms = await _roomRepository.GetAllRoomsAsync();
        var viewModels = rooms.Select(r => new RoomViewModel
        {
            id = r.id,
            nome = r.nome,
            descricao = r.descricao,
            capacidade = r.capacidade,
            categoria = r.categoria,
            status = r.status,
        }).ToList();

        return Ok(viewModels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomViewModel>> GetRoom(int id)
    {
        _logger.LogInformation($"Obter sala com ID: {id}");

        var room = await _roomRepository.GetRoomByIdAsync(id);
        if (room == null)
        {
            _logger.LogWarning($"Sala com ID {id} não encontrada.");
            return NotFound();
        }

        var viewModel = new RoomViewModel
        {
            id = room.id,
            nome = room.nome,
            descricao = room.descricao,
            capacidade = room.capacidade,
            categoria = room.categoria,
            status = room.status,
        };

        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<ActionResult<RoomViewModel>> CreateRoom([FromBody] CreateRoomDto dto)
    {
        _logger.LogInformation("Criar uma nova sala.");

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Erros de validação encontrados ao criar sala.");
            return BadRequest(ModelState);
        }

        var room = new Room
        {
            nome = dto.nome,
            descricao = dto.descricao,
            capacidade = dto.capacidade,
            categoria = dto.categoria,
            status = dto.status
        };

        await _roomRepository.AddRoomAsync(room);

        var viewModel = new RoomViewModel
        {
            id = room.id,
            nome = room.nome,
            descricao = room.descricao,
            capacidade = room.capacidade,
            categoria = room.categoria,
            status = room.status,
        };

        _logger.LogInformation($"Sala criada com ID: {room.id}");
        return CreatedAtAction(nameof(GetRoom), new { id = room.id }, viewModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] UpdateRoomDto dto)
    {
        _logger.LogInformation($"Atualizar sala com ID: {id}");

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Erros de validação encontrados ao atualizar sala.");
            return BadRequest(ModelState);
        }

        var room = await _roomRepository.GetRoomByIdAsync(id);
        if (room == null)
        {
            _logger.LogWarning($"Sala com ID {id} não encontrada.");
            return NotFound();
        }

        room.nome = dto.nome;
        room.descricao = dto.descricao;
        room.capacidade = dto.capacidade;
        room.categoria = dto.categoria;
        room.status = dto.status;

        await _roomRepository.UpdateRoomAsync(room);

        _logger.LogInformation($"Sala com ID {id} atualizada com sucesso.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        _logger.LogInformation($"Excluir sala com ID: {id}");

        var room = await _roomRepository.GetRoomByIdAsync(id);
        if (room == null)
        {
            _logger.LogWarning($"Sala com ID {id} não encontrada.");
            return NotFound();
        }

        await _roomRepository.DeleteRoomAsync(id);

        _logger.LogInformation($"Sala com ID {id} excluída com sucesso.");
        return NoContent();
    }
}

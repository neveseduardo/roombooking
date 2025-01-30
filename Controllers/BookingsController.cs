using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoomBooking.Models;
using RoomBooking.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomBooking.Models.Dto;
using RoomBooking.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace RoomBooking.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ILogger<BookingsController> _logger;

    public BookingsController(IBookingRepository bookingRepository, ILogger<BookingsController> logger)
    {
        _bookingRepository = bookingRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingViewModel>>> GetBookings()
    {
        _logger.LogInformation("Obter todas as reservas.");

        var bookings = await _bookingRepository.GetAllBookingsAsync();
        var viewModels = bookings.Select(b => new BookingViewModel
        {
            id = b.id,
            hora_inicio = b.hora_inicio,
            hora_fim = b.hora_fim,
            data_reserva = b.data_reserva,
            protocolo = b.protocolo,
            status = b.status,
            sala_id = b.sala_id,
            cliente_id = b.cliente_id,
        }).ToList();

        return Ok(viewModels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingViewModel>> GetBooking(int id)
    {
        _logger.LogInformation($"Obter reserva com ID: {id}");

        var booking = await _bookingRepository.GetBookingByIdAsync(id);
        if (booking == null)
        {
            _logger.LogWarning($"Reserva com ID {id} não encontrada.");
            return NotFound();
        }

        var viewModel = new BookingViewModel
        {
            id = booking.id,
            hora_inicio = booking.hora_inicio,
            hora_fim = booking.hora_fim,
            data_reserva = booking.data_reserva,
            protocolo = booking.protocolo,
            status = booking.status,
            sala_id = booking.sala_id,
            cliente_id = booking.cliente_id,
        };

        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<ActionResult<BookingViewModel>> CreateBooking([FromBody] CreateBookingDto dto)
    {
        _logger.LogInformation("Criar uma nova reserva.");

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Erros de validação encontrados ao criar reserva.");
            return BadRequest(ModelState);
        }

        var booking = new Booking
        {
            hora_inicio = dto.hora_inicio,
            hora_fim = dto.hora_fim,
            data_reserva = dto.data_reserva,
            status = "Pendente",
            sala_id = dto.sala_id,
            cliente_id = dto.cliente_id
        };

        await _bookingRepository.AddBookingAsync(booking);

        var viewModel = new BookingViewModel
        {
            id = booking.id,
            hora_inicio = booking.hora_inicio,
            hora_fim = booking.hora_fim,
            data_reserva = booking.data_reserva,
            protocolo = booking.protocolo,
            status = booking.status,
            sala_id = booking.sala_id,
            cliente_id = booking.cliente_id,
        };

        _logger.LogInformation($"Reserva criada com ID: {booking.id}");
        return CreatedAtAction(nameof(GetBooking), new { id = booking.id }, viewModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBooking(int id, [FromBody] UpdateBookingDto dto)
    {
        _logger.LogInformation($"Atualizar reserva com ID: {id}");

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Erros de validação encontrados ao atualizar reserva.");
            return BadRequest(ModelState);
        }

        var booking = await _bookingRepository.GetBookingByIdAsync(id);
        if (booking == null)
        {
            _logger.LogWarning($"Reserva com ID {id} não encontrada.");
            return NotFound();
        }

        booking.hora_inicio = dto.hora_inicio;
        booking.hora_fim = dto.hora_fim;
        booking.data_reserva = dto.data_reserva;
        booking.sala_id = dto.sala_id;
        booking.cliente_id = dto.cliente_id;

        await _bookingRepository.UpdateBookingAsync(booking);

        _logger.LogInformation($"Reserva com ID {id} atualizada com sucesso.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        _logger.LogInformation($"Excluir reserva com ID: {id}");

        var booking = await _bookingRepository.GetBookingByIdAsync(id);
        if (booking == null)
        {
            _logger.LogWarning($"Reserva com ID {id} não encontrada.");
            return NotFound();
        }

        await _bookingRepository.DeleteBookingAsync(id);

        _logger.LogInformation($"Reserva com ID {id} excluída com sucesso.");
        return NoContent();
    }
}
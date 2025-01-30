using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Helpers;
using RoomBooking.Repository;
using RoomBooking.Models;
using RoomBooking.Models.Dto;
using RoomBooking.Models.ViewModels;

namespace RoomBooking.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserRepository userRepository, ILogger<UserController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<UserViewModel>> GetUsers()
    {
        _logger.LogInformation("Obter todos os usuários.");

        var users = await _userRepository.GetUsersAsync();
        var viewModel = users.Select(u => new UserViewModel
        {
            id = u.id,
            name = u.name,
            email = u.email,
            roles = u.roles,
        });

        return viewModel;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            _logger.LogWarning($"Usuário com ID {id} não encontrado.");
            return NotFound();
        }

        var viewModel = new UserViewModel
        {
            id = user.id,
            name = user.name,
            email = user.email,
            roles = user.roles,
        };

        return Ok(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> StoreUser([FromBody] UserDto dto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Erros de validação encontrados ao criar usuário.");
            return BadRequest(ModelState);
        }

        var user = new User
        {
            id = dto.id,
            name = dto.name,
            email = dto.email,
            password = dto.password,
            roles = dto.roles,
        };

        user.password = PasswordHelper.HashPassword(user.password);

        await _userRepository.AddUserAsync(user);

        return CreatedAtAction("GetUser", new { id = user.id }, user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var user = await _userRepository.DeleteUserAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserDto dto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Erros de validação encontrados ao atualizar usuário.");
            return BadRequest(ModelState);
        }

        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            _logger.LogWarning($"Usuário com ID {id} não encontrado.");
            return NotFound();
        }

        var updatedUser = await _userRepository.UpdateUserAsync(id, user);

        if (updatedUser == null)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePatchUser([FromRoute] int id, [FromBody] JsonPatchDocument userDocument)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            _logger.LogWarning($"Usuário com ID {id} não encontrado.");
            return NotFound();
        }

        var updatedUser = await _userRepository.UpdateUserPatchAsync(id, userDocument);

        if (updatedUser == null)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }
}
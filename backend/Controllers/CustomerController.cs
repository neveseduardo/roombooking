using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoomBooking.Models;
using RoomBooking.Repositories;
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
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ICustomerRepository customerRepository, ILogger<CustomersController> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomers()
    {
        _logger.LogInformation("Obter todos os clientes.");

        var customers = await _customerRepository.GetAllCustomersAsync();
        var viewModels = customers.Select(c => new CustomerViewModel
        {
            id = c.id,
            nome = c.nome,
            email = c.email,
            cpf = c.cpf,
        }).ToList();

        return Ok(viewModels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerViewModel>> GetCustomer(int id)
    {
        _logger.LogInformation($"Obter cliente com ID: {id}");

        var customer = await _customerRepository.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            _logger.LogWarning($"Cliente com ID {id} não encontrado.");
            return NotFound();
        }

        var viewModel = new CustomerViewModel
        {
            id = customer.id,
            nome = customer.nome,
            email = customer.email,
            cpf = customer.cpf,
        };

        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerViewModel>> CreateCustomer([FromBody] CreateCustomerDto dto)
    {
        _logger.LogInformation("Criar um novo cliente.");

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Erros de validação encontrados ao criar cliente.");
            return BadRequest(ModelState);
        }

        var customer = new Customer
        {
            nome = dto.nome,
            email = dto.email,
            cpf = dto.cpf
        };

        await _customerRepository.AddCustomerAsync(customer);

        var viewModel = new CustomerViewModel
        {
            id = customer.id,
            nome = customer.nome,
            email = customer.email,
            cpf = customer.cpf,
        };

        _logger.LogInformation($"Cliente criado com ID: {customer.id}");
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.id }, viewModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDto dto)
    {
        _logger.LogInformation($"Atualizar cliente com ID: {id}");

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Erros de validação encontrados ao atualizar cliente.");
            return BadRequest(ModelState);
        }

        var customer = await _customerRepository.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            _logger.LogWarning($"Cliente com ID {id} não encontrado.");
            return NotFound();
        }

        customer.nome = dto.nome;
        customer.email = dto.email;
        customer.cpf = dto.cpf;

        await _customerRepository.UpdateCustomerAsync(customer);

        _logger.LogInformation($"Cliente com ID {id} atualizado com sucesso.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        _logger.LogInformation($"Excluir cliente com ID: {id}");

        var customer = await _customerRepository.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            _logger.LogWarning($"Cliente com ID {id} não encontrado.");
            return NotFound();
        }

        await _customerRepository.DeleteCustomerAsync(id);

        _logger.LogInformation($"Cliente com ID {id} excluído com sucesso.");
        return NoContent();
    }
}
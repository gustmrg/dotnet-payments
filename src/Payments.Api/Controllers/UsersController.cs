using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payments.Api.Data;
using Payments.Api.DTOs;
using Payments.Api.Models;

namespace Payments.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Lista os usuários cadastrados no banco de dados.
    /// </summary>
    /// <returns>Lista de usuários cadastrados.</returns>
    [HttpGet]
    public async Task<ActionResult<List<User>>> Get()
    {
        var users = await _context.Users
            .Include(u => u.Wallet)
            .ThenInclude(w => w.Transactions)
            .ToListAsync();

        return Ok(users);
    }
    
    /// <summary>
    /// Recebe os dados do usuário buscado pelo id, caso cadastrado no banco de dados.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Dados do usuário cadastrado de acordo com Id fornecido.</returns>
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<User>> GetById(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }

        var user = await _context.Users
            .Where(u => u.Id == id)
            .Include("Wallet")
            .FirstOrDefaultAsync();

        if (user == null) return NotFound();
        
        return Ok(user);
    }

    // Get transactions made by this user
    // [HttpGet]
    // [Route("{id:int}/transactions")]
    // public async Task<ActionResult<List<Transaction>>> GetTransactionsById(int? id)
    // {
    //     if (id == null) return BadRequest();
    //     
    //     var user = await _context.Users
    //         .Where(u => u.Id == id)
    //         .Include("Transactions")
    //         .FirstOrDefaultAsync();
    //
    //     if (user == null) return NotFound();
    //     
    //     var transactions = user.Wallet.Transactions.ToList();
    //
    //     return Ok(transactions);
    // }

    /// <summary>
    /// Cria um novo usuário.
    /// </summary>
    /// <param name="modelDto"></param>
    /// <returns>Dados do usuário cadastrado.</returns>
    [HttpPost]
    public async Task<ActionResult<User>> Create(UserDto modelDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = new User
        {
            Name = modelDto.Name,
            Document = modelDto.Document,
            Email = modelDto.Email,
            Password = modelDto.Password,
            IsCompany = modelDto.IsCompany
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return Ok(user);
    }

    /// <summary>
    /// Remove o usuário encontrado pelo id, caso cadastro no banco de dados.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete(int? id)
    {
        if (id == null) return BadRequest();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
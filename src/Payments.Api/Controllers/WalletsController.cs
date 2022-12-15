using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payments.Api.Data;
using Payments.Api.DTOs;
using Payments.Api.Models;

namespace Payments.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WalletsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Recebe os dados da carteira de dinheiro vinculada ao usuário fornecido.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Dados da carteira de dinheiro.</returns>
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Wallet>> Get([FromRoute] int id)
    {
        var wallet = await _context.Wallets
            .Include(w => w.User)
            .FirstOrDefaultAsync(w => w.Id == id);
        
        return Ok(wallet);
    }
    
    /// <summary>
    /// Cria uma carteira de dinheiro para o usuário fornecido, caso não tenha nenhuma.
    /// </summary>
    /// <returns>Dados da carteira criada.</returns>
    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<Wallet>> Create(WalletDto walletDto)
    {
        // var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == walletDto.UserId);
        // if (user == null) return NotFound();

        var wallet = new Wallet
        {
            UserId = walletDto.UserId,
            Balance = walletDto.Balance,
            IsActive = true
        };

        _context.Wallets.Add(wallet);
        await _context.SaveChangesAsync();

        return Ok(wallet);
    }

    // fix: user not showing on json response
    // [HttpGet]
    // [Route("{id:int}")]
    // public async Task<ActionResult<Wallet>> Get([FromRoute] int? id)
    // {
    //     if (id == null) return BadRequest();
    //
    //     var wallet = await _context.Wallets
    //         .Where(w => w.UserId == id)
    //         .Include(w => w.Transactions)
    //         .FirstOrDefaultAsync();
    //
    //     if (wallet == null) return NotFound();
    //
    //     return Ok(wallet);
    // }
    
    // [HttpPost]
    // [Route("{id:int}")]
    // public async Task<ActionResult<Wallet>> Create([FromRoute] int? id)
    // {
    //     if (id == null) return BadRequest();
    //
    //     var user = await _context.Users.FindAsync(id);
    //     if (user == null) return NotFound();
    //
    //     var wallet = new Wallet
    //     {
    //         UserId = user.Id,
    //         Balance = 100,
    //         User = user
    //     };
    //
    //     _context.Wallets.Add(wallet);
    //     await _context.SaveChangesAsync();
    //
    //     return Ok(user.Wallet);
    // }
}
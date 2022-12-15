using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payments.Api.Data;
using Payments.Api.DTOs;
using Payments.Api.Models;

namespace Payments.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TransactionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET ALL TRANSACTIONS BY USER ID

    // GET TRANSACTION INFO BY ID
    // [HttpGet]
    // [Route("{id:int}")]
    // public async Task<ActionResult<Transaction>> GetById(int? id)
    // {
    //     if (id == null) return BadRequest();
    //
    //     var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
    //     if (transaction == null) return NotFound();
    //     
    //     return Ok(transaction);
    // }

    // CREATE TRANSACTION
    // [HttpPost]
    // public async Task<ActionResult<Transaction>> Pay(TransactionDto transactionDto)
    // {
    //     if (!ModelState.IsValid || transactionDto.PayeeId == transactionDto.PayerId) return BadRequest();
    //
    //     var payer = await _context.Users.FindAsync(transactionDto.PayerId);
    //     var payee = await _context.Users.FindAsync(transactionDto.PayeeId);
    //     if (payer == null || payee == null) return NotFound();
    //
    //     var transaction = new Transaction
    //     {
    //         PayeeId = transactionDto.PayeeId,
    //         PayerId = transactionDto.PayerId,
    //         Amount = transactionDto.Amount,
    //         Time = DateTime.UtcNow
    //     };
    //
    //     payer.Wallet.Balance -= transaction.Amount;
    //     payer.Wallet.Transactions.Add(transaction);
    //     
    //     payee.Wallet.Balance += transaction.Amount;
    //     payee.Wallet.Transactions.Add(transaction);
    //     
    //     _context.Transactions.Add(transaction);
    //     await _context.SaveChangesAsync();
    //     
    //     return Ok(transaction);
    // }
}
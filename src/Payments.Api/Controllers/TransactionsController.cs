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

    /// <summary>
    /// Cria uma transação financeira entre dois usuários.
    /// </summary>
    /// <param name="transactionDto"></param>
    /// <returns>Transação financeira criada.</returns>
    [HttpPost]
    public async Task<ActionResult<Transaction>> Pay([FromBody] TransactionDto transactionDto)
    {
        if (!ModelState.IsValid || transactionDto.PayeeId == transactionDto.PayerId) return BadRequest();

        // Verifica se os usuários da transação estão cadastrados no sistema
        var payer = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.Id == transactionDto.PayerId);
        var payee = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.Id == transactionDto.PayeeId);
        if (payer == null || payee == null) return NotFound();


        // TODO: Verifica se os usuários têm carteira cadastrada       

        // TODO: Verifica se o saldo das carteira do pagador é maior que o valor da transferência

        // Cria a transação
        var transaction = new Transaction
        {
            PayeeId = transactionDto.PayeeId,
            PayerId = transactionDto.PayerId,
            Amount = transactionDto.Amount,
            Time = DateTime.UtcNow
        };

        // Adiciona a transação nas carteiras de ambos pagador e recebedor
        payer.Wallet.Balance -= transaction.Amount;
        payer.Wallet.Transactions.Add(transaction);

        payee.Wallet.Balance += transaction.Amount;
        payee.Wallet.Transactions.Add(transaction);

        // Salva no banco de dados
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok(transaction);
    }
}
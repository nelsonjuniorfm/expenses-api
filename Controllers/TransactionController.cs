using Microsoft.AspNetCore.Mvc;
using expenses_api.Models;

namespace expenses_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private static List<Transaction> _transactions = [
        new Transaction(){
            Id = 0,
            Description = "Initial Transaction",
            Amount = 200.0m,
            Type = "Income",
            Date = DateTime.UtcNow
        }
    ];
    private static int _idCounter = 1;

    [HttpPost]
    public IActionResult AddTransaction([FromBody] Transaction newTransaction)
    {
        newTransaction.Id = _idCounter++;
        _transactions.Add(newTransaction);
        return CreatedAtAction(nameof(GetTransaction), new { id = newTransaction.Id }, newTransaction);
    }

    [HttpGet]
    public IActionResult GetAllTransactions()
    {
        return Ok(_transactions);
    }

    [HttpGet("{id}")]
    public IActionResult GetTransaction(int id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);
        if (transaction == null) return NotFound();
        return Ok(transaction);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTransaction(int id, [FromBody] Transaction updatedTransaction)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);
        if (transaction == null) return NotFound();

        transaction.Description = updatedTransaction.Description;
        transaction.Amount = updatedTransaction.Amount;
        transaction.Type = updatedTransaction.Type;
        transaction.Date = updatedTransaction.Date;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTransaction(int id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);
        if (transaction == null) return NotFound();

        _transactions.Remove(transaction);
        return NoContent();
    }
}
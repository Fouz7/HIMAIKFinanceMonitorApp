using AutoMapper;
using HIMAIKFinanceMonitorApp.Server.Data;
using HIMAIKFinanceMonitorApp.Server.Dtos;
using HIMAIKFinanceMonitorApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace HIMAIKFinanceMonitorApp.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TransactionController(IConfiguration config) : ControllerBase
    {
        private readonly DataContext _context = new(config);

        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TransactionDto, Transaction>();
        }));

        [HttpGet("GetAllTransactions")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        [HttpGet("GetTransaction/{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        [HttpPost("AddTransaction")]
        public async Task<ActionResult<Transaction>> AddTransaction(TransactionDto transactionDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _context.Users.FindAsync(int.Parse(userId ?? ""));
            if (user == null)
            {
                return NotFound("User not found");
            }

            var transaction = _mapper.Map<Transaction>(transactionDto);
            transaction.Balance += transaction.Credit - transaction.Debit;
            transaction.CreatedBy = user.Username;
            transaction.CreatedAt = DateTime.Now;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.TransactionId }, transaction);
        }

        [HttpPut("UpdateTransaction/{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, TransactionDto transactionDto)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound("Data Not Found");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(int.Parse(userId ?? ""));
            if (user == null)
            {
                return NotFound("User not found");
            }

            transaction.Debit = transactionDto.Debit;
            transaction.Credit = transactionDto.Credit;
            transaction.Balance += transaction.Credit - transaction.Debit;
            transaction.Notes = transactionDto.Notes;
            transaction.CreatedBy = user.Username;
            transaction.CreatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTransaction", new { id = transaction.TransactionId }, transaction);
        }

        [HttpDelete("DeleteTransaction/{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return Ok("Delete Transaction Success");
        }
    }
}

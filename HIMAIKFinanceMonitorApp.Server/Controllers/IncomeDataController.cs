using HIMAIKFinanceMonitorApp.Server.Data;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using HIMAIKFinanceMonitorApp.Server.Dtos;
using HIMAIKFinanceMonitorApp.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HIMAIKFinanceMonitorApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeDataController(IConfiguration config) : ControllerBase
    {
        private readonly DataContext _context = new(config);

        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<IncomeDataDto, IncomeData>();

            cfg.CreateMap<TransactionDto, Transaction>();
        }));

        


        [HttpGet("GetAllIncomeData")]
        public async Task<ActionResult<IEnumerable<IncomeData>>> GetAllIncomeData(int? month, int? week, string nominalSortOrder = "asc")
        {
            var query = _context.IncomeData.AsQueryable();

            if (month.HasValue)
            {
                query = query.Where(data => data.TransferDate.Month == month.Value);
            }

            if (week.HasValue)
            {
                int startDay = (week.Value - 1) * 7 + 1;
                int endDay = week.Value * 7;
                query = query.Where(data => data.TransferDate.Day >= startDay && data.TransferDate.Day <= endDay);
            }

            if (nominalSortOrder == "asc")
            {
                query = query.OrderBy(data => data.Nominal);
            }
            else if (nominalSortOrder == "desc")
            {
                query = query.OrderByDescending(data => data.Nominal);
            }

            return await query.ToListAsync();
        }



        [HttpGet("GetIncome/{id}")]
        public async Task<ActionResult<IncomeData>> GetIncomeData(int id)
        {
            var incomeData = await _context.IncomeData.FindAsync(id);

            if (incomeData == null)
            {
                return NotFound();
            }

            return incomeData;
        }

        [Authorize]
        [HttpPost("AddIncomeData")]
        public async Task<ActionResult<IncomeData>> AddIncomeData(IncomeDataDto incomeDataDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _context.Users.FindAsync(int.Parse(userId ?? ""));
            if (user == null)
            {
                return NotFound("User not found");
            }

            var incomeData = _mapper.Map<IncomeData>(incomeDataDto);
            incomeData.CreatedBy = user.Fullname;
            incomeData.CreatedAt = DateTime.Now;
            _context.IncomeData.Add(incomeData);

            var transaction = _mapper.Map<Transaction>(incomeDataDto.Transaction);
            transaction.Credit = incomeDataDto.Nominal;
            transaction.Notes = $"Income from {incomeDataDto.Name}";
            transaction.Balance += transaction.Credit - transaction.Debit;
            transaction.CreatedBy = user.Fullname;
            transaction.CreatedAt = DateTime.Now;
            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetIncomeData", new { id = incomeData.Id }, incomeData);
        }

        [Authorize]
        [HttpPut("UpdateIncomeData/{id}")]
        public async Task<IActionResult> UpdateIncomeData(int id, IncomeDataDto incomeDataDto)
        {
            var incomeData = await _context.IncomeData.FindAsync(id);
            if (incomeData == null)
            {
                return NotFound("Data Not Found");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(int.Parse(userId ?? ""));
            if (user == null)
            {
                return NotFound("User not found");
            }

            incomeData.Name = incomeDataDto.Name;
            incomeData.Nominal = incomeDataDto.Nominal;
            incomeData.TransferDate = incomeDataDto.TransferDate;
            incomeData.CreatedBy = user.Fullname;
            incomeData.CreatedAt = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.IncomeData.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIncomeData", new { id = incomeData.Id }, incomeData);
        }

        [Authorize]
        [HttpDelete("DeleteIncomeData/{id}")]
        public async Task<IActionResult> DeleteIncomeData(int id)
        {
            var incomeData = await _context.IncomeData.FindAsync(id);
            if (incomeData == null)
            {
                return NotFound("Data Not FOund");
            }

            _context.IncomeData.Remove(incomeData);
            await _context.SaveChangesAsync();

            return Ok("Delete Successful");
        }

    }
}

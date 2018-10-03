using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VBApi.Data;
using VBApi.Data.Models;

namespace VBApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ExpensesController : Controller
    {
        private readonly ExpenseContext context;
        private readonly ILogger logger;

        public ExpensesController(ExpenseContext context, ILogger<ExpensesController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        // GET: api/Expenses1/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError(this.getLoggerModelState(ModelState));

                return BadRequest(ModelState);
            }

            var expense = await context.Expenses.FindAsync(Id);

            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<IActionResult> PostExpense([FromBody] Expense expense)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError(this.getLoggerModelState(ModelState));

                return BadRequest(ModelState);
            }

            // Find expense by primary keys
            var primaryExpense = await context.Expenses.FindAsync(expense.TransactionDate, expense.Recipient, expense.Currency, expense.ExpenseType);

            // Update amount if the expense is already added
            if (primaryExpense == null)
            {
                context.Expenses.Add(expense);
            }
            else
            {
                expense.Id = primaryExpense.Id;
                expense.Amount = expense.Amount;
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (expenseExists(expense.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError(this.getLoggerModelState(ModelState));

                return BadRequest(ModelState);
            }

            // Find expense by primary keys
            var expense = (from x in context.Expenses
                                  where x.Id == Id
                                  select x).FirstOrDefault();

            if (expense == null)
            {
                return NotFound();
            }

            context.Expenses.Remove(expense);
            await context.SaveChangesAsync();

            return Ok(expense);
        }

        private bool expenseExists(int id)
        {
            return context.Expenses.Any(e => e.Id == id);
        }
        private string getLoggerModelState(ModelStateDictionary modelState)
        {
            var state = string.Join(",", (from x in ModelState.Values
                                          from e in x.Errors
                                          select e.ErrorMessage).ToArray());
            return state;
        }
    }
}
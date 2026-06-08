using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Transaction;
using FinancialTrackingAPI.Dtos.User;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Interfaces;
using FinancialTrackingAPI.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FinancialTrackingAPI.Controllers
{
   [ApiController]
[Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _repository;
        public TransactionController(ITransactionRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var transactions = await _repository.GetTransactionAsync(query);
            var transactionresponse = transactions.Select(s => s.ToTransactionResponse());
            return Ok(transactionresponse);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var existing = await _repository.GetTransactionByIdAsync(id);
            if (existing == null) return NotFound();
            return Ok(existing.ToTransactionResponse());
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransactions([FromBody] TransactionRequest request)
        {
            var transaction = request.toTransactionRequest();
            await _repository.CreateAsync(transaction);
            return CreatedAtAction(nameof(GetById),
              new { id = transaction.TransactionId },
              transaction.ToTransactionResponse()
            );
        }
        [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTransactions([FromRoute] int id, [FromBody] TransactionRequest request)
        {
       var existing = await _repository.UpdateAsync(id, request.toTransactionRequest());
if (existing == null) return NotFound();
return Accepted(existing.ToTransactionResponse());
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTransactions([FromRoute] int id)
        {
         var existing = await _repository.DeleteAsync(id);
            if (!existing) return NotFound("sen sikenin adi terorist ..cocuklari ... galatasaray");

            return NoContent();
        }




    }
}
//    private readonly ITransactionRepository _repository;
//         public TransactionController(ITransactionRepository repository)
//         {
//             _repository = repository;
//         }
//         [HttpGet("/alltransactions")]
//         public async Task<IActionResult> GetTransactions([FromQuery] QueryObject queryObject)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             var transactions = await _repository.GetTransactionAsync(queryObject);

//             return Ok(transactions.Select(s => s.ToTransactionResponse()));
//         }
//         [HttpGet("{id:int}")]
//         [Route("findwithid")]
//         public async Task<IActionResult> getbyId(int id)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest();
//             }
//             var transaction = await _repository.GetTransactionByIdAsync(id);

//             if (transaction == null) return NotFound();
//             return Ok(transaction.ToTransactionResponse());

//         }

//         [HttpPost]
//         [Route("addtransaction")]
//         public async Task<IActionResult> CreateTransaction(TransactionRequest request)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest();
//             }

//             var transaction = request.toTransactionRequest();

//             await _repository.CreateAsync(transaction);
//             return CreatedAtAction(
//                 nameof(getbyId),
//                 new { id = transaction.TransactionId },
//                 transaction.ToTransactionResponse()
//             );
//         }
//         [HttpPut("{id:int}")]
//         public async Task<IActionResult> UpdateTransaction(int id, TransactionRequest request)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest();
//             }

//             var existing = await _repository.UpdateAsync(id, request.toTransactionRequest());
//             if (existing == null) return NotFound();
//             return Ok(existing.ToTransactionResponse());

//         }
//         [HttpDelete("{id:int}")]

//         public async Task<IActionResult> DeleteAsync(int id)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest();
//             }

//             var existing = await _repository.DeleteAsync(id);
//             if (!existing) return NotFound();

//             return NoContent();
//         }
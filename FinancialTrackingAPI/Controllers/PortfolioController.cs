using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Portfolio;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Interfaces;
using FinancialTrackingAPI.Mappers;
using FinancialTrackingAPI.Service.Portfolio;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTrackingAPI.Controllers
{
    [ApiController]
[Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioRepository _repository;
        private readonly IPortfolioService _service;
        public PortfolioController(IPortfolioRepository repository,IPortfolioService service)
        {
            _repository = repository;
            _service=service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {

            var portfolios = await _service.PortfolioResponsesAsync(query);
            return Ok(portfolios);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var portfolio = await _service.GetPortfolioResponseAsync(id);
            if (portfolio == null) return BadRequest();
            return Ok(portfolio);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePortfolio([FromBody] PortfolioRequest request)
        {
            var createporfolio = await _service.CreatePortfolioResponseAsync(request);
            return CreatedAtAction(nameof(GetById),
            new { id = createporfolio.PortfolioId },
            createporfolio
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePortfolio([FromRoute] int id, [FromBody] PortfolioRequest request)
        {
            var existing = await _service.UpdatePortfolioResponseAsync(id, request);
            if (existing == null) return NotFound();
            return Accepted(existing);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePortfolio([FromRoute] int id)
        {
            var existing = await _service.DeleteIsteAminakoyim(id);
            if (!existing) return BadRequest();
            return NoContent();
        }




    }
}
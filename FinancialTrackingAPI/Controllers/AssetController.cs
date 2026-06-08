using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Asset;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Interfaces;
using FinancialTrackingAPI.Mappers;
using FinancialTrackingAPI.Service.Asset;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTrackingAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepository _repository;
        private readonly IAssetService _service;
        public AssetController(IAssetRepository repository,IAssetService service)
        {
            _repository = repository;
            _service=service;
        }
        [HttpGet]
        public async Task<IActionResult> getall(QueryObject query)
        {
            var assents = await _service.GetAllAssetResponseasync(query);
            return Ok(assents);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> getasset(int id)
        {
            var asset = await _service.GetAssetResponseAsync(id);
            if (asset == null) return BadRequest();
            return Ok(asset);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] AssetRequest request)
        {
            var asset = await _service.CreateAssentResponseAsync(request);
            return CreatedAtAction(nameof(getasset),
              new { id = asset.AssetId },
              asset
              );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsset([FromRoute] int id, [FromBody] AssetRequest request)
        {
            var asset = await _service.UpdateAssetResponseAsync(id, request);
            if (asset == null) return NotFound();
            return Accepted(asset);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DelleteAsset([FromRoute] int id)
        {
            var asset = await _service.DeleteAsync(id);
            if (!asset) return NotFound();
            return NoContent();
        }





    }
}
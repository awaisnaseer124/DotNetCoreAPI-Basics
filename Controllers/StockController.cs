using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stocks;
using api.Helper;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepo = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var stock = await _stockRepo.GetAllAsycn(queryObject);
            var stockDto = stock.Select(s => s.ToStockDto());
            return Ok(stock);
        }
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetStockByIdAsycn(Id);
            if (stock == null)
            {
                return NotFound();

            }

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDTo createStockDTo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = createStockDTo.ToStockDtoFromCreate();
            await _stockRepo.CreateStockAsycn(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());

        }
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = await _stockRepo.UpdateStockAsycn(Id, updateStockRequestDto);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);

        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = await _stockRepo.DeleteStockAsync(Id);
            if (stock == null)
            {
                return NotFound();
            }

            return NoContent();

        }
    }
}
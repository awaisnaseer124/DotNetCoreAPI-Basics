using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stocks;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateStockAsycn(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stock.Remove(stockModel);

            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsycn()
        {
            return await _context.Stock.Include(c=>c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsycn(int id)
        {
            return await _context.Stock.Include(c=>c.Comments).FirstOrDefaultAsync(i=>i.Id==id);


        }

        public async Task<bool> StockExist(int id)
        {
           return  await _context.Stock.AnyAsync(s=>s.Id==id);
        }

        public async Task<Stock?> UpdateStockAsycn(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStock == null)
            {
                return null;
            }

            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Symbol = stockDto.Symbol;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

           await _context.SaveChangesAsync();
           return existingStock;
        }
    }
}
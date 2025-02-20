using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stocks;
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsycn();
        public Task<Stock?> GetStockByIdAsycn(int id);
        public Task <Stock> CreateStockAsycn(Stock  stockModel);
        public Task<Stock?> UpdateStockAsycn(int id,UpdateStockRequestDto stockDto);
        public Task<Stock?> DeleteStockAsync(int id);
        public Task<bool> StockExist(int id);
    }
}
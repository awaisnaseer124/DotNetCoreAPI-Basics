using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stocks;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                LastDiv = stockModel.LastDiv,
                MarketCap = stockModel.MarketCap,
                Industry = stockModel.Industry,
                comments=stockModel.Comments.Select(c=>c.ToCommentDto()).ToList()

            };
        }

        public static Stock ToStockDtoFromCreate(this CreateStockDTo stockdto)
        {
            return new Stock
            {
                Symbol = stockdto.Symbol,
                CompanyName = stockdto.CompanyName,
                Industry = stockdto.Industry,
                LastDiv = stockdto.LastDiv,
                MarketCap = stockdto.MarketCap

            };
        }
    }
}
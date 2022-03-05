using InfrontAssesment.Core.Interfaces;
using InfrontAssesment.Core.Models;
using InfrontAssesment.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrontAssesment.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        StockContext _context;
        public StockRepository(StockContext context)
        {
            _context = context;
        }
        public Stock AddStock(Stock stock)
        {
            var result = _context.Stocks.Add(stock);
            _context.SaveChanges();
            return result.Entity;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _context.Stocks;
        }

        public Stock GetStock(string symbol)
        {
            return _context.Stocks.FirstOrDefault(x => x.Symbol == symbol);
        }

        public void DeleteStock(Stock stock)
        {
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
        }
    }
}

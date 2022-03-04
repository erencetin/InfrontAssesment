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
        public void AddStock(Stock stock)
        {
            var result = _context.Stocks.Add(stock);
            _context.SaveChanges();
        }
    }
}

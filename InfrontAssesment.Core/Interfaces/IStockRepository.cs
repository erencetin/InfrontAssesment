using InfrontAssesment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrontAssesment.Core.Interfaces
{
    public interface IStockRepository
    {
        Stock AddStock(Stock stock);
        IEnumerable<Stock> GetAllStocks();
        Stock GetStock(string symbol);
        void DeleteStock(Stock stock);
        void SaveChanges();

    }
}

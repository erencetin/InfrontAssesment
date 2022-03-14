using InfrontAssesment.Core.Dtos;
using InfrontAssesment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrontAssesment.Core.Interfaces
{
    public interface IStockOperationService
    {
        Task<IEnumerable<StockDto>> GetStocksInPortfolio();
        Task BuyStock(string symbol, decimal buyValue, int contract);
        Task CloseStock(StockDto stock);
    }
}

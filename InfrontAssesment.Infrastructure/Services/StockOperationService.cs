using InfrontAssesment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrontAssesment.Infrastructure.Services
{
    public class StockOperationService
    {
        IStockRepository _repository;
        public StockOperationService(IStockRepository repository)
        {
            _repository = repository;
        }

        public void BuyStock(string symbol)
        {
            var stock = _repository.GetStock(symbol);
            if (stock == null)
            {
                //TODO: Get Stock info from api
                //_repository.AddStock()
            }
            else
            {

            }
        }
    }
}

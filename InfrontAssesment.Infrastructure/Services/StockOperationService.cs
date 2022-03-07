using AutoMapper;
using InfrontAssesment.Core.Dtos;
using InfrontAssesment.Core.Interfaces;
using InfrontAssesment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrontAssesment.Infrastructure.Services
{
    public class StockOperationService : IStockOperationService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IPriceDataRepository _priceDataRepository;
        private readonly IMapper _mapper;
        public StockOperationService(IStockRepository stockRepository, IPriceDataRepository priceDataRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _priceDataRepository = priceDataRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockDto>> GetStocksInPortfolio(){
            var stocksInPortfolio = _stockRepository.GetAllStocks();
            List<StockDto> stockDtos = new List<StockDto>();
            foreach (var stock in stocksInPortfolio)
            {
                var priceData = await _priceDataRepository.GetPriceData(stock.Symbol);
                var newStockDto = _mapper.Map<Stock, StockDto>(stock);
                newStockDto.CurrentPrice = priceData.Price;
                newStockDto.Name = priceData.Name;
                stockDtos.Add(newStockDto); 
            }
            return stockDtos;
        }

        public async Task BuyStock(string symbol, decimal buyValue, int contract)
        {
            var stock = _stockRepository.GetStock(symbol.ToUpper());
            var priceData = await _priceDataRepository.GetPriceData(symbol.ToUpper());
            if (stock == null)
            {
                var newStock = new Stock();
                newStock.Symbol = priceData.VwdKey;
                newStock.BuyValue = buyValue * contract;
                newStock.NumberOfContracts = contract;
                
                //TODO: Get Stock info from api
                _stockRepository.AddStock(newStock);
            }
            else
            {
                stock.BuyValue += buyValue * contract;
                stock.NumberOfContracts += contract;
            }
        }

    }
}

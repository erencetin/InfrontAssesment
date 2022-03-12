using AutoMapper;
using InfrontAssesment.Core.Dtos;
using InfrontAssesment.Core.Interfaces;
using InfrontAssesment.Core.Models;
using InfrontAssesment.Infrastructure.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrontAssesment.Test.Services
{
    public class StockOperationServiceTest
    {
        [Test]
        public async Task GetStocksInPortfolioShouldReturnCorrectList()
        {
            var stockRepositoryMock = new Mock<IStockRepository>();
            var priceRepositoryMock = new Mock<IPriceDataRepository>();
            var mapperMock = new Mock<IMapper>();
            var expectedStocks = new List<Stock>
            {
                new Stock
                {
                     Symbol = "A",
                     BuyValue = 1,
                     NumberOfContracts = 1,
                },
                new Stock
                {
                    Symbol= "B",
                    BuyValue = 2,
                    NumberOfContracts = 2,
                }
            };

            var expectedStockDtos = new List<StockDto>
            {
                new StockDto
                {
                    Symbol= "A",
                    BuyValue= 1,
                    NumberOfContracts= 1,
                },
                new StockDto {
                    Symbol= "B",
                    BuyValue = 2,
                    NumberOfContracts = 2
                }
            };

            stockRepositoryMock.Setup(s => s.GetAllStocks()).Returns(expectedStocks);
            mapperMock.Setup(s => s.Map<Stock, StockDto>(expectedStocks[0])).Returns(expectedStockDtos[0]);
            mapperMock.Setup(s => s.Map<Stock, StockDto>(expectedStocks[1])).Returns(expectedStockDtos[1]);

            priceRepositoryMock.Setup(p => p.GetPriceData("A")).ReturnsAsync(new PriceData { VwdKey = "A", Price = 3 });
            priceRepositoryMock.Setup(p => p.GetPriceData("B")).ReturnsAsync(new PriceData { VwdKey = "B", Price = 2 });

            var service = new StockOperationService(stockRepositoryMock.Object, priceRepositoryMock.Object, mapperMock.Object);
            var actual = await service.GetStocksInPortfolio();

            Assert.IsTrue(actual.Any(x => x.Symbol == "A" && x.CurrentPrice == 3));
            Assert.IsTrue(actual.Any(x => x.Symbol == "B" && x.CurrentPrice == 2));
            Assert.IsTrue(actual.Count() == 2);


        }
    }
}

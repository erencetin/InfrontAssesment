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

        [Test]
        public async Task BuyStockShouldCallSaveChangesMethodWhenWeBuyExistingStock()
        {
            var expectedStock = new Stock
            {
                Symbol = "A",
            };
            
            var priceRepositoryMock = new Mock<IPriceDataRepository>();
            var stockRepositoryMock = new Mock<IStockRepository>();
            var mapperMock = new Mock<IMapper>();
            stockRepositoryMock.Setup(p => p.GetStock("A")).Returns(expectedStock);
            priceRepositoryMock.Setup(p => p.GetPriceData("A")).ReturnsAsync(new PriceData { VwdKey = "A", Price = 3 });
            var service = new StockOperationService(stockRepositoryMock.Object, priceRepositoryMock.Object, mapperMock.Object);
            await service.BuyStock("A", 1, 1);
            stockRepositoryMock.Verify(x => x.SaveChanges(), Times.Once);

        }

        [Test]
        public async Task BuyStockShouldCallSaveChangesMethodWhenWeBuyNonExistingStock()
        {
            var priceRepositoryMock = new Mock<IPriceDataRepository>();
            var stockRepositoryMock = new Mock<IStockRepository>();
            var mapperMock = new Mock<IMapper>();

            stockRepositoryMock.Setup(x => x.GetStock("A")).Returns((Stock)null);
            priceRepositoryMock.Setup(p => p.GetPriceData("A")).ReturnsAsync(new PriceData { VwdKey = "A", Price = 3 });
            var service = new StockOperationService(stockRepositoryMock.Object, priceRepositoryMock.Object, mapperMock.Object);
            await service.BuyStock("A", 1, 1);
            stockRepositoryMock.Verify(x => x.AddStock(It.IsAny<Stock>()), Times.Once);

        }

        [Test]
        public async Task CloseStockShouldCallDeleteStockOnce()
        {
            var priceRepositoryMock = new Mock<IPriceDataRepository>();
            var stockRepositoryMock = new Mock<IStockRepository>();
            var mapperMock = new Mock<IMapper>();
            var expectedStock = new Stock
            {
                Symbol = "A"
            };

            stockRepositoryMock.Setup(x => x.GetStock("A")).Returns(expectedStock);
            var service = new StockOperationService(stockRepositoryMock.Object, priceRepositoryMock.Object, mapperMock.Object);
            await service.CloseStock("A");
            stockRepositoryMock.Verify(x => x.DeleteStock(expectedStock), Times.Once);
        }

        [Test]
        public async Task GetPriceDataShouldReturnValidPrice()
        {
            var priceRepositoryMock = new Mock<IPriceDataRepository>();
            var stockRepositoryMock = new Mock<IStockRepository>();
            var mapperMock = new Mock<IMapper>();
            var expectedPrice = new PriceData
            {
                VwdKey = "A",
                Price = 1
            };

            priceRepositoryMock.Setup(p => p.GetPriceData("A")).ReturnsAsync(expectedPrice);
            var service = new StockOperationService(stockRepositoryMock.Object, priceRepositoryMock.Object, mapperMock.Object);
            var actual = await service.GetPriceData("A");
            priceRepositoryMock.Verify(x => x.GetPriceData("A"), Times.Once);
            Assert.AreEqual(actual.Price, 1);
        }
    }
}

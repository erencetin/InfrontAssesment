using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfrontAssesment.Core.Models;
using InfrontAssesment.Infrastructure.Data;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using InfrontAssesment.Infrastructure.Repositories;
using NUnit.Framework;

namespace InfrontAssesment.Test.Repositories
{
    public class StockRepositoryTests
    {
        [Test]
        public void AddStockShouldAddEntityToContext()
        {
            var mockContext = new Mock<StockContext>();
            var mockSet = new Mock<DbSet<Stock>>();
            mockContext.Setup(m => m.Stocks).Returns(mockSet.Object);

            mockSet.Verify(m => m.Add(It.IsAny<Stock>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

        }

        [Test]
        public void GetAllStocksShouldReturnAllStoredStocks()
        {
            var contextMock = new Mock<StockContext>();
            
            //IEnumerable<Stock> stocks = new List<Stock>()
            //{
            //    new Stock()
            //    {
            //        Symbol = "A"
            //    },
            //    new Stock()
            //    {
            //        Symbol= "B"
            //    }
            //};

            //contextMock.Setup(x => x.Stocks).Returns((DbSet<Stock>)stocks);

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrontAssesment.Core.Dtos
{
    public class StockDto
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public int NumberOfContracts { get; set; }
        public decimal BuyValue { get; set; }
        public decimal CurrentValue => CurrentPrice * NumberOfContracts;
        public decimal Yield => (BuyValue - CurrentValue) * 100 / BuyValue;
    }
}

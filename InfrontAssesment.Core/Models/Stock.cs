using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrontAssesment.Core.Models
{
    public class Stock
    {
        [Key]
        public string Symbol { get; set; }
        public int NumberOfContracts { get; set; }
        public decimal BuyValue { get; set; }

    }
}

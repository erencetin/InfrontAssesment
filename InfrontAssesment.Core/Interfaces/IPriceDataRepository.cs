using InfrontAssesment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrontAssesment.Core.Interfaces
{
    public interface IPriceDataRepository
    {
        Task<PriceData> GetPriceData(string vwdKey);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InfrontAssesment.Infrastructure.Data;
using Moq;
using NUnit.Framework;

namespace InfrontAssesment.Test.Repositories
{
    public class PriceDataRepositoryTests
    {
        [Test]
        public void GetPriceDataReturnCorrectPrice()
        {
            var contextMock = new Mock<IHttpClientFactory>();
            var clientMock = new Mock<HttpClient>();
            var newRequest = new HttpRequestMessage(HttpMethod.Get,
                $"test_url");
            var responseMessageMock = new Mock<HttpResponseMessage>();
            responseMessageMock.Setup(r=>r.IsSuccessStatusCode).Returns(true);
           // responseMessageMock.Setup(r => r.Content).Returns(new HttpContent( "{\"vwdKey\": \"ABN.NL\",\"name\": \"ABN AMRO BANK N.V.\"}");
            clientMock.Setup(s => s.SendAsync(newRequest, CancellationToken.None)).ReturnsAsync(responseMessageMock.Object);

        }

    }
}

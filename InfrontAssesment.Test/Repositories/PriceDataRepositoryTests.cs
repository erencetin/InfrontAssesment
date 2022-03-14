using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InfrontAssesment.Infrastructure.Data;
using InfrontAssesment.Infrastructure.Repositories;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace InfrontAssesment.Test.Repositories
{
    public class PriceDataRepositoryTests
    {
        [Test]
        public async Task GetPriceDataReturnCorrectPrice()
        {
            TestHttpClientFactory clientFactory = new TestHttpClientFactory();
            var repository = new PriceDataRepository(clientFactory);
            var actual = await repository.GetPriceData("ABN.NL");
            Assert.AreEqual("ABN.NL", actual.VwdKey);
            Assert.AreEqual("ABN AMRO BANK N.V.", actual.Name);
        }

    }

    public class TestHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            var messageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            messageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(async (HttpRequestMessage request, CancellationToken token) => {
                    HttpResponseMessage response = new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent("{\"vwdKey\": \"ABN.NL\",\"name\": \"ABN AMRO BANK N.V.\"}")
                    };

                    return response;
                })
                .Verifiable();

            var httpClient = new HttpClient(messageHandlerMock.Object);
            return httpClient;
        }
    }
}

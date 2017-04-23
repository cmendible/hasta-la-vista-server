using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Xunit;

namespace AzureFunctions.Tests
{
    public class HttpTriggerTests
    {
        [Fact]
        public async void Run_ReturnsSvg()
        {
            //arrange
            var payload = new { Name = "Carlos Mendible", Phone = "666666666" };

            var request = CreateHttpRequestWith(payload);

            //act
            var response = await HttpTrigger.GenerateQR(request);
            var svg = await response.Content.ReadAsStringAsync();

            //assert
            Assert.NotEmpty(svg);
        }

        private HttpRequestMessage CreateHttpRequestWith(object jsonObject)
        {
            string jsonContent = JsonConvert.SerializeObject(jsonObject);

            var request = new HttpRequestMessage()
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            return request;
        }
    }
}

using OnlineShop.Services.Contracts;
using System.Text.Json;

namespace OnlineShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IConfiguration _configuration;

        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this._configuration = configuration;

            _url = _configuration.GetValue<string>("API:URL");

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
    }
}

using OnlineShop.CustomResponses;
using OnlineShop.Model;
using OnlineShop.Services.Contracts;
using System.Text.Json;

namespace OnlineShop.Services
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IConfiguration _configuration;

        public AddressService(HttpClient httpClient, IConfiguration configuration)
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

        public async Task<AddressResponse> Create(Address addressToAdd)
        {
            AddressResponse addrResponse = new AddressResponse();
            string endpoint = $"{_url}/Address/Add";

            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(endpoint, addressToAdd, _serializerOptions);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                addrResponse.Address = JsonSerializer.Deserialize<Address>(content);
                addrResponse.Status = true;

                return addrResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new AddressResponse
                {
                    Status = false,
                    Message = "Address you are Adding, Does not meet requrements"
                };
            }
            else
            {
                return new AddressResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<AddressResponse> GetAll()
        {
            AddressResponse addrResponse = new AddressResponse();
            string endpoint = $"{_url}/Addresses";

            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                addrResponse.Addresses = JsonSerializer.Deserialize<List<Address>>(content, _serializerOptions);
                addrResponse.Status = true;

                return addrResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new AddressResponse
                {
                    Status = false,
                    Message = "Address you looking for, Does not exists"
                };
            }
            else
            {
                return new AddressResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<AddressResponse> GetByCustomerId(int id)
        {
            AddressResponse addrResponse = new AddressResponse();
            string endpoint = $"{_url}/Addresses/GetByCustomerId/{id}";

            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                addrResponse.Address = JsonSerializer.Deserialize<Address>(content, _serializerOptions);
                addrResponse.Status = true;

                return addrResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new AddressResponse
                {
                    Status = false,
                    Message = "Address you looking for, Does not exists"
                };
            }
            else
            {
                return new AddressResponse
                {
                    Status = false,
                    Message = httpResponse.StatusCode + ": Error",
                };
            }
        }

        public async Task<AddressResponse> Remove(int addressIdToDelete)
        {
            AddressResponse addrResponse = new AddressResponse();
            string endpoint = $"{_url}/Addresses/Remove/{addressIdToDelete}";

            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                addrResponse.Message = JsonSerializer.Deserialize<int>(content).ToString() + " row deleted";
                addrResponse.Status = true;

                return addrResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                int orderCount = JsonSerializer.Deserialize<int>(content);
                return new AddressResponse
                {

                    Status = false,
                    Message = "Address Can Not be deleted, Here is: " + orderCount + " Sales Order, related to this product"
                };
            }
            else
            {
                return new AddressResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<AddressResponse> Update(Address addressToEdit)
        {
            AddressResponse addrResponse = new AddressResponse();
            string endpoint = $"{_url}/Addresses/Update";

            HttpResponseMessage httpResponse = await httpClient.PutAsJsonAsync(endpoint, addressToEdit, _serializerOptions);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                addrResponse.Address = JsonSerializer.Deserialize<Address>(content);
                addrResponse.Status = true;
                addrResponse.Message = "Address Successfully Updated";

                return addrResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new AddressResponse
                {
                    Status = false,
                    Message = "Address you are editing, Does not exists"
                };
            }
            else
            {
                return new AddressResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }
    }
}

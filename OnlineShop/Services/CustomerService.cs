using OnlineShop.CustomResponses;
using OnlineShop.Model;
using OnlineShop.Services.Contracts;
using System.Text.Json;

namespace OnlineShop.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IConfiguration _configuration;

        public CustomerService(HttpClient httpClient, IConfiguration configuration)
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

        public async Task<CustomerResponse> GetById(int id)
        {
            CustomerResponse custResponse = new CustomerResponse();
            string endpoint = $"{_url}/Customer/{id}";

            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                custResponse.Customer = JsonSerializer.Deserialize<Customer>(content, _serializerOptions);
                custResponse.Status = true;

                return custResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new CustomerResponse
                {
                    Status = false,
                    Message = "Customer you looking for, Does not exists"
                };
            }
            else
            {
                return new CustomerResponse
                {
                    Status = false,
                    Message = httpResponse.StatusCode + ": Error",
                };
            }
        }

        public async Task<CustomerResponse> GetAll()
        {
            CustomerResponse custResponse = new CustomerResponse();
            string endpoint = $"{_url}/Customer";

            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                custResponse.Customers = JsonSerializer.Deserialize<List<Customer>>(content, _serializerOptions);
                custResponse.Status = true;

                return custResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new CustomerResponse
                {
                    Status = false,
                    Message = "Customer you looking for, Does not exists"
                };
            }
            else
            {
                return new CustomerResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<CustomerResponse> Update(Customer CustomerToEdit)
        {
            CustomerResponse custResponse = new CustomerResponse();
            string endpoint = $"{_url}/Customer/Update";

            HttpResponseMessage httpResponse = await httpClient.PutAsJsonAsync(endpoint, CustomerToEdit, _serializerOptions);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                custResponse.Customer = JsonSerializer.Deserialize<Customer>(content);
                custResponse.Status = true;
                custResponse.Message = "Customer Successfully Updated";

                return custResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new CustomerResponse
                {
                    Status = false,
                    Message = "Customer you are editing, Does not exists"
                };
            }
            else
            {
                return new CustomerResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<CustomerResponse> Create(Customer CustomerToAdd)
        {
            CustomerResponse custResponse = new CustomerResponse();
            string endpoint = $"{_url}/Customer/Add";

            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(endpoint, CustomerToAdd, _serializerOptions);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                custResponse.Customer = JsonSerializer.Deserialize<Customer>(content);
                custResponse.Status = true;

                return custResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new CustomerResponse
                {
                    Status = false,
                    Message = "Customer you are Adding, Does not meet requrements"
                };
            }
            else
            {
                return new CustomerResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<CustomerResponse> Remove(int CustomerIdToDelete)
        {
            CustomerResponse custResponse = new CustomerResponse();
            string endpoint = $"{_url}/Customer/Remove/{CustomerIdToDelete}";

            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                custResponse.Message = JsonSerializer.Deserialize<int>(content).ToString() + " row deleted";
                custResponse.Status = true;

                return custResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                int orderCount = JsonSerializer.Deserialize<int>(content);
                return new CustomerResponse
                {

                    Status = false,
                    Message = "Customer Can Not be deleted, Here is: " + orderCount + " Sales Order, related to this Customer"
                };
            }
            else
            {
                return new CustomerResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<CustomerResponse> Take(int rowCount)
        {
            CustomerResponse custResponse = new CustomerResponse();
            string endpoint = $"{_url}/Customer/Take/{rowCount}";

            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                custResponse.Customers = JsonSerializer.Deserialize<List<Customer>>(content, _serializerOptions);
                custResponse.Status = true;

                return custResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new CustomerResponse
                {
                    Status = false,
                    Message = "Product you looking for, Does not exists"
                };
            }
            else
            {
                return new CustomerResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }
    }
}

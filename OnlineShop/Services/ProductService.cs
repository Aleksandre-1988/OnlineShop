using OnlineShop.CustomResponses;
using OnlineShop.Model;
using OnlineShop.Services.Contracts;
using System.Text.Json;

namespace OnlineShop.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductService> _logger;

        public ProductService(HttpClient httpClient, IConfiguration configuration, ILogger<ProductService> _logger)
        {
            this.httpClient = httpClient;
            this._configuration = configuration;
            this._logger = _logger;

            _url = _configuration.GetValue<string>("API:URL");

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<ProductResponse> GetById(int id)
        {
            ProductResponse prodResponse = new ProductResponse();
            string endpoint = $"{_url}/Product/{id}";

            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodResponse.Product = JsonSerializer.Deserialize<Product>(content, _serializerOptions);
                prodResponse.Status = true;

            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                prodResponse.Status = false;
                prodResponse.Message = "Product you looking for, Does not exists";
            }
            else
            {
                prodResponse.Status = false;
                prodResponse.Message = httpResponse.StatusCode + ": Error";
            }

            LogInformation(prodResponse.Message);
            return prodResponse;
        }

        public async Task<ProductResponse> Take(int maxId = 1000)
        {
            ProductResponse prodResponse = new ProductResponse();
            string endpoint = $"{_url}/Product/Take/{maxId}";

            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodResponse.Products = JsonSerializer.Deserialize<List<Product>>(content, _serializerOptions);
                prodResponse.Status = true;

                return prodResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new ProductResponse
                {
                    Status = false,
                    Message = "Product you looking for, Does not exists"
                };
            }
            else
            {
                return new ProductResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<ProductResponse> GetAll()
        {
            ProductResponse prodResponse = new ProductResponse();
            string endpoint = $"{_url}/Product";

            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodResponse.Products = JsonSerializer.Deserialize<List<Product>>(content, _serializerOptions);
                prodResponse.Status = true;

                return prodResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new ProductResponse
                {
                    Status = false,
                    Message = "Product you looking for, Does not exists"
                };
            }
            else
            {
                return new ProductResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<ProductCategoryResponse> GetCategories()
        {
            ProductCategoryResponse prodCategoryResponse = new ProductCategoryResponse();
            string endpoint = $"{_url}/Product/Categories/GetAll";

            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodCategoryResponse.ProductCategories = JsonSerializer.Deserialize<List<ProductCategory>>(content, _serializerOptions);
                prodCategoryResponse.Status = true;

                return prodCategoryResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new ProductCategoryResponse
                {
                    Status = false,
                    Message = "ProductCategory you looking for, Does not exists"
                };
            }
            else
            {
                return new ProductCategoryResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<ProductModelResponse> GetModels()
        {
            ProductModelResponse prodModelResponse = new ProductModelResponse();
            string endpoint = $"{_url}/ProductModel";

            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodModelResponse.ProductModels = JsonSerializer.Deserialize<List<ProductModel>>(content, _serializerOptions);
                prodModelResponse.Status = true;

                return prodModelResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new ProductModelResponse
                {
                    Status = false,
                    Message = "ProductModel you looking for, Does not exists"
                };
            }
            else
            {
                return new ProductModelResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<ProductResponse> Update(Product productToEdit)
        {
            ProductResponse prodResponse = new ProductResponse();
            string endpoint = $"{_url}/Product/Update";

            HttpResponseMessage httpResponse = await httpClient.PutAsJsonAsync(endpoint, productToEdit, _serializerOptions);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodResponse.Product = JsonSerializer.Deserialize<Product>(content);
                prodResponse.Status = true;
                prodResponse.Message = "Product Successfully Updated";

                return prodResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new ProductResponse
                {
                    Status = false,
                    Message = "Product you are editing, Does not exists"
                };
            }
            else
            {
                return new ProductResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<ProductResponse> Create(Product productToAdd)
        {
            ProductResponse prodResponse = new ProductResponse();
            string endpoint = $"{_url}/Product/Add";

            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(endpoint, productToAdd, _serializerOptions);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodResponse.Product = JsonSerializer.Deserialize<Product>(content);
                prodResponse.Message = "Product Successfully Created";
                prodResponse.Status = true;

                return prodResponse;
            }
            else
            {
                return new ProductResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<ProductResponse> Remove(int productIdToDelete)
        {
            ProductResponse prodResponse = new ProductResponse();
            string endpoint = $"{_url}/Product/Remove/{productIdToDelete}";

            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodResponse.Message = JsonSerializer.Deserialize<int>(content).ToString() + " row deleted";
                prodResponse.Status = true;

                return prodResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                int orderCount = JsonSerializer.Deserialize<int>(content);
                return new ProductResponse
                {

                    Status = false,
                    Message = "Product Can Not be deleted, Here is: " + orderCount + " Sales Order, related to this product"
                };
            }
            else
            {
                return new ProductResponse
                {
                    Status = false,
                    Message = httpResponse.ReasonPhrase
                };
            }
        }

        public async Task<ProductResponse> ProdNameExists(string prodName)
        {
            ProductResponse prodResponse = new ProductResponse();
            string endpoint = $"{_url}/Product/Checkname";

            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(endpoint, prodName, _serializerOptions);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                prodResponse.Status = JsonSerializer.Deserialize<int>(content) == 1 ? true : false;
                return prodResponse;
            }
            else
            {
                _logger.LogInformation("Error in ProductService: Cant execute CheckProdName()");

                return null;
            }

        }

        private void LogInformation(string message)
        {
            _logger.LogInformation(DateTime.Now.ToLongTimeString() + ": " + message);
        }
    }
}
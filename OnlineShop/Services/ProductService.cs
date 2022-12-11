using OnlineShop.Services.Contracts;
using OnlineShop.Model;
using System.Text.Json;
using OnlineShop.CustomResponses;

namespace OnlineShop.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IConfiguration _configuration;

        public ProductService(HttpClient httpClient, IConfiguration configuration)
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

        public async Task<ProductResponse> Take(int rowCount)
        {
            ProductResponse prodResponse = new ProductResponse();
            string endpoint = $"{_url}/Product/Take/{rowCount}";
            
            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodResponse.Products = JsonSerializer.Deserialize<List<Product>>(content, _serializerOptions);
                prodResponse.Status = true;

                return prodResponse;
            }
            else if(httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
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
            else if(httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
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
            string endpoint = $"{_url}/Product";
            
            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodCategoryResponse.ProductCategories = JsonSerializer.Deserialize<List<ProductCategory>>(content, _serializerOptions);
                prodCategoryResponse.Status = true;

                return prodCategoryResponse;
            }
            else if(httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
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
            string endpoint = $"{_url}/Product";
            
            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodModelResponse.ProductModels = JsonSerializer.Deserialize<List<ProductModel>>(content, _serializerOptions);
                prodModelResponse.Status = true;

                return prodModelResponse;
            }
            else if(httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
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
            
            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(endpoint, productToEdit, _serializerOptions);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodResponse.Product = JsonSerializer.Deserialize<Product>(content);
                prodResponse.Status = true;
                prodResponse.Message = "Product Successfully Updated";

                return prodResponse;
            }
            else if(httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
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
            
            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(endpoint,productToAdd,_serializerOptions);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodResponse.Product = JsonSerializer.Deserialize<Product>(content);
                prodResponse.Status = true;

                return prodResponse;
            }
            else if(httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new ProductResponse
                {
                    Status = false,
                    Message = "Product you are Adding, Does not meet requrements"
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

        public async Task<ProductResponse> Remove(int productIdToDelete)
        {
            ProductResponse prodResponse = new ProductResponse();
            string endpoint = $"{_url}/Product/Remove{productIdToDelete}";
            
            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodResponse.Message = JsonSerializer.Deserialize<int>(content).ToString() + " row deleted";
                prodResponse.Status = true;

                return prodResponse;
            }
            else if(httpResponse.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                int orderCount = JsonSerializer.Deserialize<int>(content);
                return new ProductResponse
                {

                    Status = false,
                    Message = "Product Can Not be deleted, Here is: "+orderCount+" Sales Order, related to this product"
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
    }
}
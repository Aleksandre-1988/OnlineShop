using OnlineShop.CustomResponses;
using OnlineShop.Model;
using OnlineShop.Services.Contracts;
using System.Text.Json;

namespace OnlineShop.Services
{
    public class ProductCategoryService : IProductCategoryService
    {

        private readonly HttpClient httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IConfiguration _configuration;

        public ProductCategoryService(HttpClient httpClient, IConfiguration configuration)
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

        public async Task<ProductCategoryResponse> Add(ProductCategory productCategoryToAdd)
        {
            ProductCategoryResponse prodCategoryResponse = new ProductCategoryResponse();
            string endpoint = $"{_url}/ProductCategory/Add";

            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(endpoint, productCategoryToAdd);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodCategoryResponse.Message = content;
                prodCategoryResponse.Status = true;

                return prodCategoryResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new ProductCategoryResponse
                {
                    Status = false,
                    Message = "Sorry, Error was thrown, Please try again"
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

        public async Task<ProductCategoryResponse> Delete(int id)
        {
            ProductCategoryResponse prodCategoryResponse = new ProductCategoryResponse();
            string endpoint = $"{_url}/ProductCategory/Remove/{id}";

            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(endpoint);

            if (httpResponse.IsSuccessStatusCode)
            {
                prodCategoryResponse.Message = $"Product Category with ID: {id} Syccesfylly Deleted";
                prodCategoryResponse.Status = true;

                return prodCategoryResponse;
            }
            else
            {
                return new ProductCategoryResponse
                {
                    Status = false,
                    Message = $"Product Category with ID: {id} Could not Deleted",
            };
            }
        }

        public async Task<ProductCategoryResponse> Edit(ProductCategory productCategoryToEdit)
        {
            ProductCategoryResponse prodCategoryResponse = new ProductCategoryResponse();
            string endpoint = $"{_url}/ProductCategory/Update";

            HttpResponseMessage httpResponse = await httpClient.PutAsJsonAsync(endpoint, productCategoryToEdit);

            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                prodCategoryResponse.Message = content;
                prodCategoryResponse.Status = true;

                return prodCategoryResponse;
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new ProductCategoryResponse
                {
                    Status = false,
                    Message = "Sorry, Error was thrown, Please try again"
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

        public async Task<ProductCategoryResponse> GetProductCategories()
        {
            ProductCategoryResponse prodCategoryResponse = new ProductCategoryResponse();
            string endpoint = $"{_url}/ProductCategory/GetAll";

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
    }
}

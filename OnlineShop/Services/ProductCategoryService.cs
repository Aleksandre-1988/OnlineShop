﻿using OnlineShop.CustomResponses;
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

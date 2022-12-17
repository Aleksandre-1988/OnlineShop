using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Interface;
using OnlineShop.Api.Model_Views;
using OnlineShop.Domain.Model;
using AutoMapper;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IUnitOfWork unit, IMapper mapper, ILogger<ProductController> logger)
        {
            this._unit = unit;
            this._mapper = mapper;
            this._logger = logger;
        }

        // GET: api/<ProductController>/Take/5
        [HttpGet]
        [Route("Take/{rowCount:int}")]
        public IActionResult Take(int maxId = 1000 )//Default Take 10
        {
            List<Product> productList = _unit.productRep
                .Find(x=> x.ProductID < 1000,null, "SalesOrderDetails")
                .ToList();

            if (productList != null)
            {
                List<Product_View> product_Views = _mapper.Map<List<Product>, List<Product_View>>(productList);
                return Ok(product_Views);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> productList = _unit.productRep
                .Find(x => x.ProductID < 1000, null, "SalesOrderDetails")
                .ToList();

            if (productList != null)
            {
                List<Product_View> product_Views = _mapper.Map<List<Product>, List<Product_View>>(productList);

                return Ok(product_Views);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var product = _unit.productRep.Get(id);

            if (product != null)
            {
                Product_View product_View = _mapper.Map<Product_View>(product);
                return Ok(product_View);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ProductController>/Add
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Product productToAdd)
        {
            if (productToAdd == null)
            {
                return BadRequest();
            }

            try
            {
                Product product = _mapper.Map<Product>(productToAdd);
                product.ModifiedDate = DateTime.Now;
                _unit.productRep.Add(product);
                _unit.Save();

                return Ok(productToAdd);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error: Can't Save data into DataBase");
            }
        }

        // POST api/<ProductController>/Update
        [HttpPost("Update")]
        public IActionResult Update([FromBody] Product productToEdit)
        {
            try
            {
                Product product = _mapper.Map<Product>(productToEdit);
                product.ModifiedDate = DateTime.Now;
                _unit.productRep.Update(product);
                _unit.Save();
                return Ok(productToEdit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Update data into DataBase");
            }
        }

        // DELETE api/<ProductController>/Remove/5
        [HttpDelete("Remove/{id:int}")]
        public IActionResult Remove(int id)
        {
            if (id <= 0)
                return BadRequest("Product id is not valid");

            var order = _unit.salesOrderDetailRep.Find(x => x.ProductID == id);
            if(order.Any())
            {
                return Conflict(order.Count());
            }
            try
            {
                _unit.productRep.Remove(id);
                int rowsAffected = _unit.Save();
                return Ok(rowsAffected);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error: Can't Delete data from DataBase");
            }
        }

        // GET: api/<ProductController>
        [HttpGet("Models/Getall")]
        public IActionResult Getall()
        {
            var productModels = _unit.productModelRep.GetAll();

            if (productModels != null)
            {
                return Ok(productModels);
            }
            else
            {
                return NotFound();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Interface;
using OnlineShop.Api.Models;
using OnlineShop.Domain.Model;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected readonly IUnitOfWork _unit;

        public ProductController(IUnitOfWork unit)
        {
            this._unit = unit;
        }

        // GET: api/<ProductController>/Take/5
        [HttpGet]
        [Route("Take/{rowCount:int}")]
        public IActionResult Take(int rowCount = 10 )//Default Take 10
        {
            var productList = _unit.productRep.Take(rowCount);

            if (productList != null)
            {
                return Ok(productList);
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
            var productList = _unit.productRep.GetAll();

            if (productList != null)
            {
                return Ok(productList);
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
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ProductController>/Add
        [HttpPost]
        public IActionResult Create([FromBody] Product productToAdd)
        {
            if (productToAdd == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.productRep.Add(productToAdd);
                _unit.Save();

                return Ok(productToAdd);
            }
            catch (Exception ex)
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
                _unit.productRep.Update(productToEdit);
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
                _unit.Save();
                return Ok(order.Count());
            }
            catch (Exception ex)
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

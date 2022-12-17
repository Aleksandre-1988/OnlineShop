using Microsoft.AspNetCore.Mvc;
using OnlineShop.API.Filters;
using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Log]
    public class ProductModelController : ControllerBase
    {
        protected readonly IUnitOfWork _unit;

        public ProductModelController(IUnitOfWork unit)
        {
            this._unit = unit;
        }

        // GET: api/<ProductModel>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ProductModel> prodModelList = _unit.productModelRep.GetAll().ToList();

            if (prodModelList != null)
            {
                return Ok(prodModelList);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<ProductModel>/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var prodModel = _unit.productModelRep.Get(id);

            if (prodModel != null)
            {
                return Ok(prodModel);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ProductModel>
        [HttpPost]
        public IActionResult Add([FromBody] ProductModel productModelToAdd)
        {
            if (productModelToAdd == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.productModelRep.Add(productModelToAdd);
                _unit.Save();

                return Ok(productModelToAdd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Save data into DataBase");
            }
        }

        // PUT api/<ProductModel>/5
        [HttpPut]
        public IActionResult Update([FromBody] ProductModel productModelToEdit)
        {
            var prodCategory = Get(productModelToEdit.ProductModelID);

            if (prodCategory == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.productModelRep.Update(productModelToEdit);
                _unit.Save();
                return Ok(productModelToEdit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Update data into DataBase");
            }
        }

        // DELETE api/<ProductModel>/5
        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            var prodModel = Get(id);

            if (prodModel == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.productModelRep.Remove(id);
                _unit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Delete data into DataBase");
            }
        }
    }
}

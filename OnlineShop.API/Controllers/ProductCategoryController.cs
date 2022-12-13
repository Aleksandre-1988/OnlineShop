using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        protected readonly IUnitOfWork _unit;

        public ProductCategoryController(IUnitOfWork unit)
        {
            this._unit = unit;
        }

        // GET: api/<ProductCategory>/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var prodCategoryList = _unit.productCategoryRep.GetAll();

            if (prodCategoryList != null)
            {
                return Ok(prodCategoryList);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<ProductCategory>/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var prodCategory = _unit.productCategoryRep.Get(id);

            if (prodCategory != null)
            {
                return Ok(prodCategory);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ProductCategory>/Add
        [HttpPost]
        public IActionResult Add([FromBody] ProductCategory productCategoryToAdd)
        {
            if (productCategoryToAdd == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.productCategoryRep.Add(productCategoryToAdd);
                _unit.Save();

                return Ok(productCategoryToAdd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Save data into DataBase");
            }
        }

        // PUT api/<ProductCategory>/5
        [HttpPut]
        public IActionResult Update([FromBody] ProductCategory productCategoryToEdit)
        {
            var prodCategory = Get(productCategoryToEdit.ProductCategoryID);

            if (prodCategory == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.productCategoryRep.Update(productCategoryToEdit);
                _unit.Save();
                return Ok(productCategoryToEdit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Update data into DataBase");
            }
        }

        // DELETE api/<ProductCategory>/5
        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            var prodCategory = Get(id);

            if (prodCategory == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.productCategoryRep.Remove(id);
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

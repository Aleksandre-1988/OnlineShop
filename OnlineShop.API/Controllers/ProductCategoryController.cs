using Microsoft.AspNetCore.Mvc;
using OnlineShop.Api.Model;
using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;
using OnlineShop.API.Extension;

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
            var prodCategoryList = _unit.productCategoryRep.Find(null,null,"Products").ToList();

            List<ProductCategory_View> prodCat_View = new List<ProductCategory_View>();
            prodCat_View.ConvertToProductCategoryPart(prodCategoryList);

            if (prodCat_View != null)
            {
                return Ok(prodCat_View);
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
        [HttpPost("Add")]
        public IActionResult Add([FromBody] ProductCategory_View productCategoryToAdd)
        {
            ProductCategory prodCategory = new ProductCategory();
            Extensions.MapToProductCategory(prodCategory, productCategoryToAdd);

            try
            {
                _unit.productCategoryRep.Add(prodCategory);
                _unit.Save();

                return Ok(prodCategory.Name);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Save data into DataBase");
            }
        }

        // PUT api/<ProductCategory>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] ProductCategory_View productCategoryToEdit)
        {
            ProductCategory prodCategory = _unit.productCategoryRep.Get(productCategoryToEdit.ProductCategoryID);

            Extensions.MapToProductCategory(prodCategory, productCategoryToEdit);
            try
            {
                _unit.productCategoryRep.Update(prodCategory);
                _unit.Save();
                return Ok(prodCategory.Name +" Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Update data into DataBase");
            }
        }

        // DELETE api/<ProductCategory>/5
        [HttpDelete("Remove/{id:int}")]
        public IActionResult Remove(int id)
        {
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

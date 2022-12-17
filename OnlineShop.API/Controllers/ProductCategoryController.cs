using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;
using OnlineShop.API.Model_Views;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        protected readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressesController> _logger;

        public ProductCategoryController(IUnitOfWork unit, ILogger<AddressesController> logger, IMapper mapper)
        {
            this._unit = unit;
            this._mapper = mapper;
            this._logger = logger;
        }

        // GET: api/<ProductCategory>/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            List<ProductCategory> prodCategoryList = _unit.productCategoryRep.Find(null,null,"Products").ToList();

            List<ProductCategory_View> prodCat_View = _mapper.Map<List<ProductCategory>, List<ProductCategory_View>>(prodCategoryList);

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
        public IActionResult Add([FromBody] ProductCategory_View productCategoryViewToAdd)
        {
            if(productCategoryViewToAdd == null)
            {
                return BadRequest();
            }

            try
            {
                ProductCategory productCategory = _mapper.Map<ProductCategory>(productCategoryViewToAdd);
                productCategory.ModifiedDate = DateTime.Now;

                _unit.productCategoryRep.Add(productCategory);
                _unit.Save();

                return Ok(productCategoryViewToAdd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Save data into DataBase");
            }
        }

        // PUT api/<ProductCategory>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] ProductCategory_View productCategoryViewToEdit)
        {
            if(productCategoryViewToEdit == null)
            {
                return BadRequest();
            }

            try
            {
                ProductCategory productCategory = _mapper.Map<ProductCategory>(productCategoryViewToEdit);
                productCategory.ModifiedDate = DateTime.Now;

                _unit.productCategoryRep.Update(productCategory);
                _unit.Save();
                return Ok(productCategoryViewToEdit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Update data into DataBase");
            }
        }

        // DELETE api/<ProductCategory>/5
        [HttpDelete("Remove/{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                if(await RemoveParentCategoryId(id))
                {
                    _unit.productCategoryRep.Remove(id);
                    _unit.Save();
                    return Ok();

                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return StatusCode(500, "Internal Server Error: Can't Delete data into DataBase");
            }
        }
        private async Task<bool> RemoveParentCategoryId(int id)
        {
            try
            {
                var categories = Task.Run(() =>_unit.productCategoryRep.Find(x => x.ParentProductCategoryID == id));
                foreach (var category in await categories)
                {
                    category.ParentProductCategoryID = null;
                }
                _unit.Save();
                return true;
            }
            catch 
            {
                return false;
            }
            
        }
    }
}

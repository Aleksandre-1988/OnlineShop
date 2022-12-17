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
    public class SalesOrderHeaderController : ControllerBase
    {
        protected readonly IUnitOfWork _unit;

        public SalesOrderHeaderController(IUnitOfWork unit)
        {
            this._unit = unit;
        }
        // GET: api/<SalesOrderHeaderController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var salesOrderHeader = _unit.salesOrderHeaderRep.GetAll().ToList();

            if (salesOrderHeader != null)
            {
                return Ok(salesOrderHeader);
            }
            else
            {
                return NotFound(salesOrderHeader);
            }
        }

        // GET api/<SalesOrderHeaderController>/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var salesOrderHeader = _unit.salesOrderHeaderRep.Get(id);

            if (salesOrderHeader != null)
            {
                return Ok(salesOrderHeader);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<SalesOrderHeaderController>
        [HttpPost]
        public IActionResult Add([FromBody] SalesOrderHeader salesOrderHeaderToAdd)
        {
            if (salesOrderHeaderToAdd == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.salesOrderHeaderRep.Add(salesOrderHeaderToAdd);
                _unit.Save();

                return Ok(salesOrderHeaderToAdd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Save data into DataBase");
            }
        }

        // PUT api/<SalesOrderHeaderController>/5
        [HttpPut]
        public IActionResult Update([FromBody] SalesOrderHeader salesOrderHeaderToEdit)
        {
            var salesOrderHeader = Get(salesOrderHeaderToEdit.SalesOrderID);

            if(salesOrderHeader == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.salesOrderHeaderRep.Update(salesOrderHeaderToEdit);
                _unit.Save();
                return Ok(salesOrderHeaderToEdit);
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Internal Server Error: Can't Update data into DataBase");
            }
        }

        // DELETE api/<SalesOrderHeaderController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            var salesOrderHeader = Get(id);

            if(salesOrderHeader == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.salesOrderHeaderRep.Remove(id);
                _unit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Internal Server Error: Can't Delete data into DataBase");
            }
        }
    }
}

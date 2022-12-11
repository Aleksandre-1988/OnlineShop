using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        protected readonly IUnitOfWork _unit;

        public CustomerController(IUnitOfWork unit)
        {
            this._unit = unit;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var customerList = _unit.customerRep.GetAll().ToList();

            if (customerList != null)
            {
                return Ok(customerList);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var customer = _unit.customerRep.Get(id);

            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Add([FromBody] Customer customerToAdd)
        {
            if (customerToAdd == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.customerRep.Add(customerToAdd);
                _unit.Save();

                return Ok(customerToAdd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Save data into DataBase");
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut]
        public IActionResult Update([FromBody] Customer customerToEdit)
        {
            var customer = Get(customerToEdit.CustomerID);

            if (customer == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.customerRep.Update(customerToEdit);
                _unit.Save();
                return Ok(customerToEdit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Update data into DataBase");
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            var customer = Get(id);

            if (customer == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.customerRep.Remove(id);
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
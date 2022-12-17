using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Api.Model_Views;
using OnlineShop.API.Model_Views;
using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        protected readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IUnitOfWork unit, IMapper mapper, ILogger<CustomerController> logger)
        {
            this._unit = unit;
            this._mapper = mapper;
            this._logger = logger;
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
                Customer_View customer_View = _mapper.Map<Customer_View>(customer);
                return Ok(customer_View);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/<CustomerController>/Take/5
        [HttpGet]
        [Route("Take/{rowCount:int}")]
        public IActionResult Take(int rowCount = 10)//Default Take 10
        {
            List<Customer> customerList = _unit.customerRep.Take(rowCount).ToList();
            if (customerList != null)
            {
                List<Customer_View> customer_Views = _mapper.Map<List<Customer>, List<Customer_View>>(customerList);
                return Ok(customer_Views);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<CustomerController>/Add
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Customer customerViewToAdd)
        {
            if (customerViewToAdd == null)
            {
                return BadRequest();
            }

            try
            {
                Customer customer = _mapper.Map<Customer>(customerViewToAdd);
                customer.ModifiedDate = DateTime.Now;

                _unit.customerRep.Add(customer);
                _unit.Save();

                return Ok(customer);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error: Can't Save data into DataBase");
            }
        }

        // PUT api/<CustomerController>/
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Customer customerViewToEdit)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(customerViewToEdit);
                customer.ModifiedDate = DateTime.Now;

                _unit.customerRep.Update(customer);
                _unit.Save();
                return Ok(customerViewToEdit);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error: Can't Update data into DataBase");
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            var customer = _unit.customerRep.Get(id);

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
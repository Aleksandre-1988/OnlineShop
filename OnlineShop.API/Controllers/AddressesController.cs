using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Api.Model_Views;
using OnlineShop.API.Extension;
using OnlineShop.API.Model_Views;
using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        protected readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(IUnitOfWork unit, ILogger<AddressesController> logger, IMapper mapper)
        {
            this._unit = unit;
            this._logger = logger;
            this._mapper = mapper;
        }

        // GET: api/<AddressesController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var addresses = _unit.addressRep.GetAll().ToList();
            List<Address_View> address_Views = _mapper.Map<List<Address>, List<Address_View>>(addresses);

            if (addresses != null)
            {
                return Ok(address_Views);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<AddressesController>/5
        [HttpGet("GetByCustomerId/{id:int}")]
        public IActionResult Get(int id)
        {
            List<Address> addresssList = _unit.addressRep.GetAddressListByCustomerId(id);


            if (addresssList != null)
            {
                Address_View address_View = _mapper.Map<Address_View>(addresssList);

                return Ok(address_View);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<AddressesController>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Address addressViewToAdd)
        {
            if (addressViewToAdd == null)
            {
                return BadRequest();
            }

            try
            {
                Address address = _mapper.Map<Address>(addressViewToAdd);
                address.ModifiedDate = DateTime.Now;

                _unit.addressRep.Add(address);
                _unit.Save();

                return Ok(addressViewToAdd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Save data into DataBase");
            }
        }

        // PUT api/<AddressesController>/Update
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Address_View addressViewToEdit)
        {
            try
            {
                Address address = _mapper.Map<Address>(addressViewToEdit);
                address.ModifiedDate = DateTime.Now;

                _unit.addressRep.Update(address);
                _unit.Save();
                return Ok(addressViewToEdit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Update data into DataBase");
            }
        }

        // DELETE api/<AddressesController>/5
        [HttpDelete("Remove/{id:int}")]
        public IActionResult Remove(int id)
        {
            var address = Get(id);

            if (address == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.addressRep.Remove(id);
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

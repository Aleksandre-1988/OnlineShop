using Microsoft.AspNetCore.Mvc;
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

        public AddressesController(IUnitOfWork unit)
        {
            this._unit = unit;
        }
        // GET: api/<AddressesController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var addresses = _unit.addressRep.GetAll().ToList();

            if (addresses != null)
            {
                return Ok(addresses);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<AddressesController>/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var address = _unit.addressRep.Get(id);

            if (address != null)
            {
                return Ok(address);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<AddressesController>
        [HttpPost]
        public IActionResult Add([FromBody] Address addressToAdd)
        {
            if (addressToAdd == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.addressRep.Add(addressToAdd);
                _unit.Save();

                return Ok(addressToAdd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Save data into DataBase");
            }
        }

        // PUT api/<AddressesController>/5
        [HttpPut]
        public IActionResult Update([FromBody] Address addressToEdit)
        {
            var address = Get(addressToEdit.AddressID);

            if (address == null)
            {
                return BadRequest();
            }

            try
            {
                _unit.addressRep.Update(addressToEdit);
                _unit.Save();
                return Ok(addressToEdit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: Can't Update data into DataBase");
            }
        }

        // DELETE api/<AddressesController>/5
        [HttpDelete("{id:int}")]
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

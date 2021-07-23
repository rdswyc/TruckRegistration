using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TruckRegistration.Domain;

namespace TruckRegistration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TruckController : ControllerBase
    {
        private readonly TruckService _service;

        public TruckController(TruckService service)
        {
            _service = service;
        }

        [Route("{id:int}", Name = "TruckItem")]
        [HttpGet]
        [ProducesResponseType(typeof(TruckViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetTruck([FromRoute] int id)
        {
            var truck = _service.Get(id);
            return Ok(truck);
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TruckViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<IEnumerable<TruckViewModel>> GetTrucks()
        {
            var trucks = _service.GetAll();
            return Ok(trucks);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<TruckViewModel> PostTruck([FromBody] TruckViewModel truck)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _service.Add(truck);
            return CreatedAtRoute("TruckItem", new { model.Id }, model);
        }

        [Route("{id:int}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult PutTruck([FromRoute] int id, [FromBody] TruckViewModel truck)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Edit(id, truck);
            return NoContent();
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult DeleteTruck([FromRoute] int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}

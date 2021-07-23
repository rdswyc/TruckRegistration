using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TruckRegistration.Domain;

namespace TruckRegistration.Controllers
{
    /// <summary>
    /// Main controller for the truck aggregate root.
    /// This is a simple bound entity with no dependencies, with actions to perform based on the HTTP methods.
    /// Also, to prevent logic in this layer, all the logging and exception handling are handed over to the depending service.
    /// This also justifies the fact that there are no unit tests for this controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TruckController : ControllerBase
    {
        private readonly TruckService _service;

        public TruckController(TruckService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// HTTP Get to return a truck from Id. eg route: GET api/Truck/1
        /// </summary>
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

        /// <summary>
        /// HTTP Get to return all trucks. route: api/Truck
        /// </summary>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TruckViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<IEnumerable<TruckViewModel>> GetTrucks()
        {
            var trucks = _service.GetAll();
            return Ok(trucks);
        }

        /// <summary>
        /// HTTP Post to add a new truck. route: api/Truck
        /// </summary>
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

        /// <summary>
        /// HTTP Put to edit a truck. eg route: PUT api/Truck/1
        /// </summary>
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

        /// <summary>
        /// HTTP Delete to delete a truck. eg route: DELETE api/Truck/1
        /// </summary>
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

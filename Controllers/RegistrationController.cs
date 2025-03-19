using EventsManagementApp.Models;
using EventsManagementApp.Service;
using EventsManagementsApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EventsManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly RegistrationService _registrationService;

        public RegistrationController(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        public IEnumerable<Registration> Get() => _registrationService.GetAllRegistrations();

        [HttpGet("{id}")]
        public ActionResult<Registration> Get(Guid id) => _registrationService.GetRegistrationById(id);

        [HttpPost]
        public IActionResult Post([FromBody] RegistrationDTO registration)
        {
            _registrationService.CreateRegistration(registration);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] RegistrationDTO registration)
        {
            _registrationService.UpdateRegistration(id,registration);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _registrationService.DeleteRegistration(id);
            return Ok();
        }
    }
}

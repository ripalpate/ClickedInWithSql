using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickedInSql.Data;
using ClickedInSql.Models;
using ClickedInSql.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClickedInSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        readonly ServiceRepository _serviceRepository;
        readonly CreateServiceRequestValidator _validator;

        public ServicesController()
        {
            _validator = new CreateServiceRequestValidator();
            _serviceRepository = new ServiceRepository();
        }
        [HttpPost]
        public ActionResult AddInterest(CreateServiceRequest createRequest)
        {
            if (_validator.ValidateInterest(createRequest))
            {
                return BadRequest(new { error = "users must have an interest name" });
            }

            var newService = _serviceRepository.AddService(createRequest.Name, createRequest.Description, createRequest.Price);
            return Created($"api/{newService}", newService);
        }

        [HttpGet]
        public ActionResult GetAllServices()
        {
            var interests = _serviceRepository.GetAllServices();

            return Ok(interests);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteService(int id)
        {
            _serviceRepository.DeleteService(id);
            return Ok();
        }
    }
}
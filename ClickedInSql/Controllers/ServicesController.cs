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
        readonly UpdateServiceRequestValidator _updateValidator;

        public ServicesController()
        {
            _validator = new CreateServiceRequestValidator();
            _serviceRepository = new ServiceRepository();
            _updateValidator = new UpdateServiceRequestValidator();
        }
        [HttpPost]
        public ActionResult AddInterest(CreateServiceRequest createRequest)
        {
            if (_validator.ValidateService(createRequest))
            {
                return BadRequest(new { error = "users must have an service name" });
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

        [HttpPut("{id}")]
        public ActionResult UpdateService(int id, UpdateServiceRequest updateRequest)
        {
            if (_updateValidator.ValidateService(updateRequest))
            {
                return BadRequest(new { error = "users must have an service name" });
            }
            _serviceRepository.UpdateService(id, updateRequest.Name, updateRequest.Description, updateRequest.Price);
            return Ok("Service Updated");
        }
    }
}
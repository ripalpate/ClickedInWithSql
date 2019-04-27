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
    public class InterestsController : ControllerBase
    {
        readonly InterestRepository _interestRepository;
        readonly CreateInterestRequestValidator _validator;

        public InterestsController()
        {
            _validator = new CreateInterestRequestValidator();
            _interestRepository = new InterestRepository();
        }
        [HttpPost]
        public ActionResult AddInterest(CreateInterestRequest createRequest)
        {
            if (_validator.ValidateInterest(createRequest))
            {
                return BadRequest(new { error = "users must have an interest name" });
            }

            var newInterest = _interestRepository.AddInterest(createRequest.Name);
            return Created($"api/{newInterest}", newInterest);
        }

        [HttpGet]
        public ActionResult GetAllInterests()
        {
            var interests = _interestRepository.GetAllInterests();

            return Ok(interests);
        }
    }
}
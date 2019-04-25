using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickedInSql.Data;
using ClickedInSql.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClickedInSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersServicesController : ControllerBase
    {
        readonly UsersServicesRepository _usersServicesRepository;

        public UsersServicesController()
        {
            _usersServicesRepository = new UsersServicesRepository();
        }
        [HttpPost]
        public ActionResult AddUsersInterests(CreateUsersServicesRequest createRequest)
        {
            var newUserService = _usersServicesRepository.AddUsersServices(createRequest.UserId, createRequest.ServiceId);
            return Created($"api/{newUserService}", newUserService);
        }
    }
}
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
    public class UsersInterestsController : ControllerBase
    {
        readonly UsersInterestsRepository _usersInterestsRepository;

        public UsersInterestsController()
        {
            _usersInterestsRepository = new UsersInterestsRepository();
        }
        [HttpPost]
        public ActionResult AddUsersInterests(CreateUsersInterestsRequest createRequest)
        {
            var newUserInterest = _usersInterestsRepository.AddUsersInterests(createRequest.UserId, createRequest.InterestId);
            return Created($"api/{newUserInterest}", newUserInterest);
        }
    }
}
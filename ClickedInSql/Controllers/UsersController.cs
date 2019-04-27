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
    public class UsersController : ControllerBase
    {
        readonly UserRepository _userRepository;
        readonly CreateUserRequestValidator _validator;
        public UsersController()
        {
            _validator = new CreateUserRequestValidator();
            _userRepository = new UserRepository();

        }

        [HttpPost("register")]

        public ActionResult AddUser(CreateUserRequest createRequest)
        {
            if (_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "users must have a name, release date, age and is prisioner" });
            }

            var newUser = _userRepository.AddUser(createRequest.Name, createRequest.ReleaseDate, createRequest.Age, createRequest.IsPrisoner);

            return Created($"api/users/{newUser.Id}", newUser);
        }


        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
             _userRepository.DeleteUser(id);
            return Ok();
        }
    }
}

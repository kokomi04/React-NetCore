using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.ViewModels;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;

        private static List<User> users =new();
        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(users);
        }

        [HttpPost]
        public IActionResult AddUser(UserVM userVM)
        {
            User user = _mapper.Map<User>(userVM);
            users.Add(user);


            return Ok(users);
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            var userVMs = users.AsQueryable().ProjectTo<UserVM>(_mapper.ConfigurationProvider);
            return Ok(userVMs);
        }

        [HttpPost]
        public IActionResult AddUser(List<UserVM> userVMs)
        {
            //var user = _mapper.Map<User>(userVM);
            //users.Add(user);

            var urs = userVMs.AsQueryable().ProjectTo<User>(_mapper.ConfigurationProvider).AsEnumerable();
            users.AddRange(urs);
            return Ok(users);
        }
    }
}

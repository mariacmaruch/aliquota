using Aliquota.API.Models;
using Aliquota.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliquota.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _user;
        private readonly IMapper _mapper; 

        public UserController(ILogger<UserController> logger, IUserService user, IMapper mapper)
        {
            _logger = logger;
            _user = user;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _user.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _user.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }

        [HttpPost]
        [Route("add")]
        public IActionResult Create(UserModel user)
        {
            try
            {
                var usuario = UserModel.ConvertToDto(user, user.Conta);
                var result = _user.Create(usuario);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }

        [HttpPut]
        [Route("atualizar/{id}")]
        public IActionResult Update(int id, UserUpdateModel user)
        {
            try
            {
                var usuario = UserUpdateModel.CovertToDto(user);

                var result = _user.Update(id, usuario);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }
    }
}

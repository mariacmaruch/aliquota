using Aliquota.API.Models;
using Aliquota.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliquota.API.Controllers
{
    [Route("api/conta")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;
        private readonly IMapper _mapper;
        private readonly ILogger<ContaController> _logger;

        public ContaController(IContaService contaService, IMapper mapper, ILogger<ContaController> logger)
        {
            _contaService = contaService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("depositar")]
        public IActionResult Depositar(ContaModel conta)
        {
            try
            {
                var contaDto = ContaModel.ConvertToDto(conta);

                var result = _contaService.Depositar(contaDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw ;
            }
        }
    }
}

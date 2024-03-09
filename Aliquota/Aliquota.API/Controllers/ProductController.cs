using Aliquota.API.Models;
using Aliquota.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliquota.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("aplicar")]
        public IActionResult Aplicar(ProductModel produto)
        {
            try
            {
                var dto = ProductModel.ConvertToDto(produto);

                var result = _productService.Aplicar(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPut]
        [Route("resgatar/{id}")]
        public IActionResult Resgatar(int id, ProductUpdateModel produto)
        {
            try
            {
                var dto = ProductUpdateModel.ConvertToDto(produto);

                var result = _productService.Resgatar(id, dto);

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

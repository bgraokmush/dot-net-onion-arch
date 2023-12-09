using ETicaretApi.Application.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_TicaretApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet("get")]
        public IActionResult GetAll()
        {
            return Ok(_productReadRepository.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Price = 100,
                    Stock = 100
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Price = 200,
                    Stock = 200
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    Price = 300,
                    Stock = 300
                },
            });
            await _productWriteRepository.SaveAsync();

            return Ok(_productReadRepository.GetAll());
        }
    }
}

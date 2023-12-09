using ETicaretApi.Application.Repositories.OrderRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_TicaretApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderWriteRepository _orderWriteRepository;

        public OrderController(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Test()
        {
            _orderWriteRepository.AddAsync(new(){Adress = "Adress 1",Description = "Description 1",});
            _orderWriteRepository.AddAsync(new(){Adress = "Adress 2",Description = "Description 2",});

            await _orderWriteRepository.SaveAsync();
            return Ok();
        }
    }
}

using BlazesoftChallenge_Ashran.Repositories;
using BlazesoftChallenge_Ashran.Requests;
using BlazesoftChallenge_Ashran.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazesoftChallenge_Ashran.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("spin")]
        public async Task<IActionResult> Spin([FromBody] SpinRequest request)
        {
            var result = await _gameService.SpinAsync(request.Bet);
            return Ok(result);
        }

        [HttpPost("update-balance")]
        public async Task<IActionResult> AddBalance([FromBody] AddBalanceRequest request)
        {
            var newBalance = await _gameService.AddBalanceAsync(request.amount);
            return Ok(new { balance = newBalance });
        }

    }
}

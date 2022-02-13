using CharpJson.Service;
using CharpJson.Service.Interface;
using CharpJson.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace CharpJson.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly ICoreService _coreService;

        public UpdateController(IOptionsMonitor<Command> setting, ICoreService coreService)
            => _coreService = coreService;

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] object update)
            => Ok(await _coreService.ExecuteAsync(update));


        [HttpGet("/start")]
        public async Task<IActionResult> Start()
            => Ok(await _coreService.Start());
    }
}
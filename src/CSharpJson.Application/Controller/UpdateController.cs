using System.Threading.Tasks;
using CSharpJson.Infrastructure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CSharpJson.Application.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateController : ControllerBase
    {
            private readonly ICoreService _coreService;

            public UpdateController(ICoreService coreService)
                => _coreService = coreService;

            [HttpPost]
            public async Task<IActionResult> Update([FromBody] object update)
                => Ok(await _coreService.ExecuteAsync(update));

            [HttpGet("/start")]
            public async Task<IActionResult> Start()
                => Ok(await _coreService.SetWebHook());
    }
}
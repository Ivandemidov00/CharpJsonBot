using CSharpJson.Application.Verification;
using Flurl.Http;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace CSharpJson.Application.Core;

public class CoreServiceVerificationProxy : ICoreService
{
    private readonly CoreService _coreService;
    private readonly IIdentificationService _identificationService;
    public CoreServiceVerificationProxy(CoreService coreService, IIdentificationService identificationService)
    {
        _coreService = coreService;
        _identificationService = identificationService;
    }

    public async Task<string> ExecuteAsync(object update)
    {
        var updateDto = JsonConvert.DeserializeObject<Update>(update.ToString());
        var typeMessage = await _identificationService.CheckType(updateDto.Message?.Text ?? string.Empty);
        return await _coreService.ExecuteAsync(updateDto, typeMessage);
    }

    public Task<IFlurlResponse> SetWebHook()
        => _coreService.SetWebHook();
}
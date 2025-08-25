using LightlessSync.API.Dto.Group;
using LightlessSync.PlayerData.Pairs;
using LightlessSync.Services.Mediator;
using LightlessSync.Services.ServerConfiguration;
using LightlessSync.UI;
using LightlessSync.UI.Components.Popup;
using LightlessSync.WebAPI;
using Microsoft.Extensions.Logging;

namespace LightlessSync.Services;

public class UiFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly LightlessMediator _lightlessMediator;
    private readonly ApiController _apiController;
    private readonly UiSharedService _uiSharedService;
    private readonly PairManager _pairManager;
    private readonly ServerConfigurationManager _serverConfigManager;
    private readonly LightlessProfileManager _lightlessProfileManager;
    private readonly PerformanceCollectorService _performanceCollectorService;

    public UiFactory(ILoggerFactory loggerFactory, LightlessMediator lightlessMediator, ApiController apiController,
        UiSharedService uiSharedService, PairManager pairManager, ServerConfigurationManager serverConfigManager,
        LightlessProfileManager lightlessProfileManager, PerformanceCollectorService performanceCollectorService)
    {
        _loggerFactory = loggerFactory;
        _lightlessMediator = lightlessMediator;
        _apiController = apiController;
        _uiSharedService = uiSharedService;
        _pairManager = pairManager;
        _serverConfigManager = serverConfigManager;
        _lightlessProfileManager = lightlessProfileManager;
        _performanceCollectorService = performanceCollectorService;
    }

    public SyncshellAdminUI CreateSyncshellAdminUi(GroupFullInfoDto dto)
    {
        return new SyncshellAdminUI(_loggerFactory.CreateLogger<SyncshellAdminUI>(), _lightlessMediator,
            _apiController, _uiSharedService, _pairManager, dto, _performanceCollectorService);
    }

    public StandaloneProfileUi CreateStandaloneProfileUi(Pair pair)
    {
        return new StandaloneProfileUi(_loggerFactory.CreateLogger<StandaloneProfileUi>(), _lightlessMediator,
            _uiSharedService, _serverConfigManager, _lightlessProfileManager, _pairManager, pair, _performanceCollectorService);
    }

    public PermissionWindowUI CreatePermissionPopupUi(Pair pair)
    {
        return new PermissionWindowUI(_loggerFactory.CreateLogger<PermissionWindowUI>(), pair,
            _lightlessMediator, _uiSharedService, _apiController, _performanceCollectorService);
    }
}

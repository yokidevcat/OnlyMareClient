using OnlyMare.API.Dto.Group;
using OnlyMare.PlayerData.Pairs;
using OnlyMare.Services.Mediator;
using OnlyMare.Services.ServerConfiguration;
using OnlyMare.UI;
using OnlyMare.UI.Components.Popup;
using OnlyMare.WebAPI;
using Microsoft.Extensions.Logging;

namespace OnlyMare.Services;

public class UiFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly OnlyMareMediator _onlymareMediator;
    private readonly ApiController _apiController;
    private readonly UiSharedService _uiSharedService;
    private readonly PairManager _pairManager;
    private readonly ServerConfigurationManager _serverConfigManager;
    private readonly OnlyMareProfileManager _onlymareProfileManager;
    private readonly PerformanceCollectorService _performanceCollectorService;

    public UiFactory(ILoggerFactory loggerFactory, OnlyMareMediator onlymareMediator, ApiController apiController,
        UiSharedService uiSharedService, PairManager pairManager, ServerConfigurationManager serverConfigManager,
        OnlyMareProfileManager onlymareProfileManager, PerformanceCollectorService performanceCollectorService)
    {
        _loggerFactory = loggerFactory;
        _onlymareMediator = onlymareMediator;
        _apiController = apiController;
        _uiSharedService = uiSharedService;
        _pairManager = pairManager;
        _serverConfigManager = serverConfigManager;
        _onlymareProfileManager = onlymareProfileManager;
        _performanceCollectorService = performanceCollectorService;
    }

    public SyncshellAdminUI CreateSyncshellAdminUi(GroupFullInfoDto dto)
    {
        return new SyncshellAdminUI(_loggerFactory.CreateLogger<SyncshellAdminUI>(), _onlymareMediator,
            _apiController, _uiSharedService, _pairManager, dto, _performanceCollectorService);
    }

    public StandaloneProfileUi CreateStandaloneProfileUi(Pair pair)
    {
        return new StandaloneProfileUi(_loggerFactory.CreateLogger<StandaloneProfileUi>(), _onlymareMediator,
            _uiSharedService, _serverConfigManager, _onlymareProfileManager, _pairManager, pair, _performanceCollectorService);
    }

    public PermissionWindowUI CreatePermissionPopupUi(Pair pair)
    {
        return new PermissionWindowUI(_loggerFactory.CreateLogger<PermissionWindowUI>(), pair,
            _onlymareMediator, _uiSharedService, _apiController, _performanceCollectorService);
    }
}

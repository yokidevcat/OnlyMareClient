using Dalamud.Game.Command;
using Dalamud.Plugin.Services;
using OnlyMare.FileCache;
using OnlyMare.OnlyMareConfiguration;
using OnlyMare.OnlyMareConfiguration.Models;
using OnlyMare.Services.Mediator;
using OnlyMare.Services.ServerConfiguration;
using OnlyMare.UI;
using OnlyMare.WebAPI;
using System.Globalization;

namespace OnlyMare.Services;

public sealed class CommandManagerService : IDisposable
{
    private const string _commandName = "/onlymare";

    private readonly ApiController _apiController;
    private readonly ICommandManager _commandManager;
    private readonly OnlyMareMediator _mediator;
    private readonly OnlyMareConfigService _onlymareConfigService;
    private readonly PerformanceCollectorService _performanceCollectorService;
    private readonly CacheMonitor _cacheMonitor;
    private readonly ServerConfigurationManager _serverConfigurationManager;

    public CommandManagerService(ICommandManager commandManager, PerformanceCollectorService performanceCollectorService,
        ServerConfigurationManager serverConfigurationManager, CacheMonitor periodicFileScanner,
        ApiController apiController, OnlyMareMediator mediator, OnlyMareConfigService onlymareConfigService)
    {
        _commandManager = commandManager;
        _performanceCollectorService = performanceCollectorService;
        _serverConfigurationManager = serverConfigurationManager;
        _cacheMonitor = periodicFileScanner;
        _apiController = apiController;
        _mediator = mediator;
        _onlymareConfigService = onlymareConfigService;
        _commandManager.AddHandler(_commandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "Opens the OnlyMare UI" + Environment.NewLine + Environment.NewLine +
                "Additionally possible commands:" + Environment.NewLine +
                "\t /onlymare toggle - Disconnects from OnlyMare, if connected. Connects to OnlyMare, if disconnected" + Environment.NewLine +
                "\t /onlymare toggle on|off - Connects or disconnects to OnlyMare respectively" + Environment.NewLine +
                "\t /onlymare gpose - Opens the OnlyMare Character Data Hub window" + Environment.NewLine +
                "\t /onlymare analyze - Opens the OnlyMare Character Data Analysis window" + Environment.NewLine +
                "\t /onlymare settings - Opens the OnlyMare Settings window"
        });
    }

    public void Dispose()
    {
        _commandManager.RemoveHandler(_commandName);
    }

    private void OnCommand(string command, string args)
    {
        var splitArgs = args.ToLowerInvariant().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

        if (splitArgs.Length == 0)
        {
            // Interpret this as toggling the UI
            if (_onlymareConfigService.Current.HasValidSetup())
                _mediator.Publish(new UiToggleMessage(typeof(CompactUi)));
            else
                _mediator.Publish(new UiToggleMessage(typeof(IntroUi)));
            return;
        }

        if (!_onlymareConfigService.Current.HasValidSetup())
            return;

        if (string.Equals(splitArgs[0], "toggle", StringComparison.OrdinalIgnoreCase))
        {
            if (_apiController.ServerState == WebAPI.SignalR.Utils.ServerState.Disconnecting)
            {
                _mediator.Publish(new NotificationMessage("OnlyMare disconnecting", "Cannot use /toggle while OnlyMare is still disconnecting",
                    NotificationType.Error));
            }

            if (_serverConfigurationManager.CurrentServer == null) return;
            var fullPause = splitArgs.Length > 1 ? splitArgs[1] switch
            {
                "on" => false,
                "off" => true,
                _ => !_serverConfigurationManager.CurrentServer.FullPause,
            } : !_serverConfigurationManager.CurrentServer.FullPause;

            if (fullPause != _serverConfigurationManager.CurrentServer.FullPause)
            {
                _serverConfigurationManager.CurrentServer.FullPause = fullPause;
                _serverConfigurationManager.Save();
                _ = _apiController.CreateConnectionsAsync();
            }
        }
        else if (string.Equals(splitArgs[0], "gpose", StringComparison.OrdinalIgnoreCase))
        {
            _mediator.Publish(new UiToggleMessage(typeof(CharaDataHubUi)));
        }
        else if (string.Equals(splitArgs[0], "rescan", StringComparison.OrdinalIgnoreCase))
        {
            _cacheMonitor.InvokeScan();
        }
        else if (string.Equals(splitArgs[0], "perf", StringComparison.OrdinalIgnoreCase))
        {
            if (splitArgs.Length > 1 && int.TryParse(splitArgs[1], CultureInfo.InvariantCulture, out var limitBySeconds))
            {
                _performanceCollectorService.PrintPerformanceStats(limitBySeconds);
            }
            else
            {
                _performanceCollectorService.PrintPerformanceStats();
            }
        }
        else if (string.Equals(splitArgs[0], "medi", StringComparison.OrdinalIgnoreCase))
        {
            _mediator.PrintSubscriberInfo();
        }
        else if (string.Equals(splitArgs[0], "analyze", StringComparison.OrdinalIgnoreCase))
        {
            _mediator.Publish(new UiToggleMessage(typeof(DataAnalysisUi)));
        }
        else if (string.Equals(splitArgs[0], "settings", StringComparison.OrdinalIgnoreCase))
        {
            _mediator.Publish(new UiToggleMessage(typeof(SettingsUi)));
        }
    }
}
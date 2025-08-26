using OnlyMare.API.Dto.User;
using OnlyMare.PlayerData.Pairs;
using OnlyMare.Services.Mediator;
using OnlyMare.Services.ServerConfiguration;
using Microsoft.Extensions.Logging;

namespace OnlyMare.PlayerData.Factories;

public class PairFactory
{
    private readonly PairHandlerFactory _cachedPlayerFactory;
    private readonly ILoggerFactory _loggerFactory;
    private readonly OnlyMareMediator _onlymareMediator;
    private readonly ServerConfigurationManager _serverConfigurationManager;

    public PairFactory(ILoggerFactory loggerFactory, PairHandlerFactory cachedPlayerFactory,
        OnlyMareMediator onlymareMediator, ServerConfigurationManager serverConfigurationManager)
    {
        _loggerFactory = loggerFactory;
        _cachedPlayerFactory = cachedPlayerFactory;
        _onlymareMediator = onlymareMediator;
        _serverConfigurationManager = serverConfigurationManager;
    }

    public Pair Create(UserFullPairDto userPairDto)
    {
        return new Pair(_loggerFactory.CreateLogger<Pair>(), userPairDto, _cachedPlayerFactory, _onlymareMediator, _serverConfigurationManager);
    }

    public Pair Create(UserPairDto userPairDto)
    {
        return new Pair(_loggerFactory.CreateLogger<Pair>(), new(userPairDto.User, userPairDto.IndividualPairStatus, [], userPairDto.OwnPermissions, userPairDto.OtherPermissions),
            _cachedPlayerFactory, _onlymareMediator, _serverConfigurationManager);
    }
}
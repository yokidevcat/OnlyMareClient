using LightlessSync.FileCache;
using LightlessSync.Services.Mediator;
using LightlessSync.WebAPI.Files;
using Microsoft.Extensions.Logging;

namespace LightlessSync.PlayerData.Factories;

public class FileDownloadManagerFactory
{
    private readonly FileCacheManager _fileCacheManager;
    private readonly FileCompactor _fileCompactor;
    private readonly FileTransferOrchestrator _fileTransferOrchestrator;
    private readonly ILoggerFactory _loggerFactory;
    private readonly LightlessMediator _lightlessMediator;

    public FileDownloadManagerFactory(ILoggerFactory loggerFactory, LightlessMediator lightlessMediator, FileTransferOrchestrator fileTransferOrchestrator,
        FileCacheManager fileCacheManager, FileCompactor fileCompactor)
    {
        _loggerFactory = loggerFactory;
        _lightlessMediator = lightlessMediator;
        _fileTransferOrchestrator = fileTransferOrchestrator;
        _fileCacheManager = fileCacheManager;
        _fileCompactor = fileCompactor;
    }

    public FileDownloadManager Create()
    {
        return new FileDownloadManager(_loggerFactory.CreateLogger<FileDownloadManager>(), _lightlessMediator, _fileTransferOrchestrator, _fileCacheManager, _fileCompactor);
    }
}
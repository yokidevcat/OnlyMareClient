using OnlyMare.FileCache;
using OnlyMare.Services.Mediator;
using OnlyMare.WebAPI.Files;
using Microsoft.Extensions.Logging;

namespace OnlyMare.PlayerData.Factories;

public class FileDownloadManagerFactory
{
    private readonly FileCacheManager _fileCacheManager;
    private readonly FileCompactor _fileCompactor;
    private readonly FileTransferOrchestrator _fileTransferOrchestrator;
    private readonly ILoggerFactory _loggerFactory;
    private readonly OnlyMareMediator _onlymareMediator;

    public FileDownloadManagerFactory(ILoggerFactory loggerFactory, OnlyMareMediator onlymareMediator, FileTransferOrchestrator fileTransferOrchestrator,
        FileCacheManager fileCacheManager, FileCompactor fileCompactor)
    {
        _loggerFactory = loggerFactory;
        _onlymareMediator = onlymareMediator;
        _fileTransferOrchestrator = fileTransferOrchestrator;
        _fileCacheManager = fileCacheManager;
        _fileCompactor = fileCompactor;
    }

    public FileDownloadManager Create()
    {
        return new FileDownloadManager(_loggerFactory.CreateLogger<FileDownloadManager>(), _onlymareMediator, _fileTransferOrchestrator, _fileCacheManager, _fileCompactor);
    }
}
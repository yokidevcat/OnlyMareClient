using OnlyMare.API.Data;
using OnlyMare.FileCache;
using OnlyMare.Services.CharaData.Models;

namespace OnlyMare.Services.CharaData;

public sealed class OnlyMareCharaFileDataFactory
{
    private readonly FileCacheManager _fileCacheManager;

    public OnlyMareCharaFileDataFactory(FileCacheManager fileCacheManager)
    {
        _fileCacheManager = fileCacheManager;
    }

    public OnlyMareCharaFileData Create(string description, CharacterData characterCacheDto)
    {
        return new OnlyMareCharaFileData(_fileCacheManager, description, characterCacheDto);
    }
}
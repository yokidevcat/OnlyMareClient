using LightlessSync.API.Data;
using LightlessSync.FileCache;
using LightlessSync.Services.CharaData.Models;

namespace LightlessSync.Services.CharaData;

public sealed class LightlessCharaFileDataFactory
{
    private readonly FileCacheManager _fileCacheManager;

    public LightlessCharaFileDataFactory(FileCacheManager fileCacheManager)
    {
        _fileCacheManager = fileCacheManager;
    }

    public LightlessCharaFileData Create(string description, CharacterData characterCacheDto)
    {
        return new LightlessCharaFileData(_fileCacheManager, description, characterCacheDto);
    }
}
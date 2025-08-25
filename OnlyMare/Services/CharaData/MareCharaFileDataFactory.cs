using OnlyMare.API.Data;
using OnlyMare.FileCache;
using OnlyMare.Services.CharaData.Models;

namespace OnlyMare.Services.CharaData;

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
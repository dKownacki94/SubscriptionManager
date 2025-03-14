using SubscriptionManager.Application.Interfaces;

namespace SubscriptionManager.Infrastructure.Services;

public class DbPathProvider : IDbPathProvider
{
    private readonly IFileSystemService _fileSystemService;

    public DbPathProvider(IFileSystemService fileSystemService)
    {
        _fileSystemService = fileSystemService;
    }

    public string GetDbPath()
    {
        var basePath = _fileSystemService.GetAppDataDirectory();
        return Path.Combine(basePath, "subscriptions.db");
    }
}

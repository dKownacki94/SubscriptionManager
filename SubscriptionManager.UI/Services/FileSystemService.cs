using SubscriptionManager.Application.Interfaces;

namespace SubscriptionManager.UI.Services;

public class FileSystemService : IFileSystemService
{
    public string GetAppDataDirectory()
    {
        return FileSystem.AppDataDirectory;
    }
}
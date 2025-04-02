using SubscriptionManager.Application.Interfaces;

namespace SubscriptionManager.Infrastructure.Services;

public class AvatarService : IAvatarService
{
    private readonly IFileSystemService _fileSystemService;
    private const string CustomAvatarsDirectoryName = "CustomAvatars";

    private readonly List<string> _defaultAvatarFileNames =
    [
        "logo_netflix.png",
        "logo_prime.png",
        "logo_xbox.png",
        "logo_upload.png"
    ];

    public AvatarService(IFileSystemService fileSystemService)
    {
        _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        EnsureCustomAvatarsDirectoryExists();
    }

    public IEnumerable<string> GetAvailableAvatars()
    {
        var customAvatarPaths = GetCustomAvatarPaths();
        return _defaultAvatarFileNames.Concat(customAvatarPaths).Distinct();
    }

    private IEnumerable<string> GetCustomAvatarPaths()
    {
        var customAvatarsDir = GetCustomAvatarsDirectoryPath();

        if (!Directory.Exists(customAvatarsDir))
        {
            return [];
        }

        try
        {
            return Directory.EnumerateFiles(customAvatarsDir)
                            .Where(filePath => HasSupportedImageExtension(filePath))
                            .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error accessing custom avatars directory '{customAvatarsDir}': {ex.Message}");
            return Enumerable.Empty<string>();
        }
    }

    private static bool HasSupportedImageExtension(string filePath)
    {
        string? extension = Path.GetExtension(filePath)?.ToLowerInvariant();
        return extension != null && (extension == ".png" || extension == ".jpg" || extension == ".jpeg");
    }

    private string GetCustomAvatarsDirectoryPath()
    {
        var appDataDir = _fileSystemService.GetAppDataDirectory();
        return Path.Combine(appDataDir, CustomAvatarsDirectoryName);
    }

    private void EnsureCustomAvatarsDirectoryExists()
    {
        var customAvatarsDir = GetCustomAvatarsDirectoryPath();
        try
        {
            if (!Directory.Exists(customAvatarsDir))
            {
                Directory.CreateDirectory(customAvatarsDir);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating custom avatars directory '{customAvatarsDir}': {ex.Message}");
        }
    }
}
namespace SubscriptionManager.Helpers
{
    public static class FileHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename);
        }
    }
}

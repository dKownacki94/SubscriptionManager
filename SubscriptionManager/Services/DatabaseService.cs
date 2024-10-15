using SQLite;
using SubscriptionManager.Helpers;
using SubscriptionManager.Models;

namespace SubscriptionManager.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = FileHelper.GetLocalFilePath("subscriptions.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Subscription>().Wait();
        }

        public Task<List<Subscription>> GetSubscriptionsAsync()
        {
            return _database.Table<Subscription>().ToListAsync();
        }

        public Task<Subscription> GetSubscriptionAsync(int id)
        {
            return _database.Table<Subscription>().FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> SaveSubscriptionAsync(Subscription subscription)
        {
            if (subscription.Id != 0)
            {
                return _database.UpdateAsync(subscription);
            }
            else
            {
                return _database.InsertAsync(subscription);
            }
        }

        public Task<int> DeleteSubscriptionAsync(Subscription subscription)
        {
            return _database.DeleteAsync(subscription);
        }
    }
}

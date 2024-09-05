using NotificationCompress.Helps;
using NotificationCompress.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationCompress.Services
{
    public class LocalDatabase
    {
        SQLiteAsyncConnection Database;

        public LocalDatabase()
        {
            
        }

        async Task Init()
        {
            if(Database is not null)
            {
                return;
            }

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants
                .Flags);
            await Database.EnableWriteAheadLoggingAsync();
            await Database.CreateTableAsync<RuleAction>();
        }

        public async Task<RuleAction> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<RuleAction>()
                .Where(i => i.Id ==  id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RuleAction>> GetActionsAsync()
        {
            await Init();
            return await Database.Table<RuleAction>()
                .ToListAsync();
        }

        public async Task<int> SaveActionAsync(RuleAction action)
        {
            await Init();
            if(action.Id != 0)
            {
                return await Database.UpdateAsync(action);
            }
            else
            {
                return await Database.InsertAsync(action);
            }
        }

        public async Task<int> DeleteActionAsync(RuleAction action)
        {
            await Init();
            return await Database.DeleteAsync(action);
        }
    }
}

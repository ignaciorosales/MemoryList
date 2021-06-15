using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLiteNetExtensionsAsync.Extensions;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using memoryList.Models;

namespace memoryList
{
    public class DatabaseService
    {
        readonly SQLiteAsyncConnection _db;

        public DatabaseService()
        {
            _db = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SIRODB.db3"));

            InitializeAsync().SafeFireAndForget(false);
        }

        private async Task InitializeAsync()
        {
            await _db.CreateTableAsync<Item>();
            await _db.CreateTableAsync<Todo>();
            await _db.CreateTableAsync<Other>();
        }

        public async Task<List<T>> GetAllWithChildrenAsync<T>(bool recursive = true) where T : new()
        {
            var result = await _db.GetAllWithChildrenAsync<T>(recursive: recursive);
            return result;
        }

        public async Task<List<T>> GetAllWithChildrenAsync<T>(Expression<Func<T, bool>> predicate, bool recursive = true) where T : new()
        {
            var result = await _db.GetAllWithChildrenAsync(predicate, recursive);
            return result;
        }

        public async Task<T> GetWithChildrenAsync<T>(int id) where T : new()
        {
            var result = await _db.GetWithChildrenAsync<T>(id, true);
            return result;
        }

        public async Task<List<T>> GetAllAsync<T>() where T : new()
        {
            var result = await _db.Table<T>().ToListAsync();
            return result;
        }

        public async Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            var result = await _db.Table<T>().Where(predicate).ToListAsync();
            return result;
        }

        public async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            try
            {
                var result = await _db.FindAsync(predicate);
                return result;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public async Task InsertAsync<T>(T item) where T : new()
        {
            await _db.InsertAsync(item);
        }

        public async Task InsertOrReplaceWithChildrenAsync<T>(T item) where T : new()
        {
            await _db.InsertOrReplaceWithChildrenAsync(item, true);
        }

        public async Task InsertOrReplaceAllWithChildrenAsync<T>(List<T> items) where T : new()
        {
            await _db.InsertOrReplaceAllWithChildrenAsync(items, true);
        }

        public async Task UpdateAsync<T>(T item) where T : new()
        {
            await _db.UpdateAsync(item);
        }

        public async Task UpdateWithChildrenAsync<T>(T item) where T : new()
        {
            await _db.UpdateWithChildrenAsync(item);
        }

        public async Task DeleteAsync<T>(T item) where T : new()
        {
            await _db.DeleteAsync(item, false);
        }

        public async Task DeleteWithChildrenAsync<T>(T item) where T : new()
        {
            await _db.DeleteAsync(item, true);
        }

        public async Task DeleteAllAsync<T>() where T : new()
        {
            await _db.DeleteAllAsync<T>();
        }

        public async Task DeleteAllAsync<T>(List<T> items) where T : new()
        {
            await _db.DeleteAllAsync(items, true);
        }
    }
}

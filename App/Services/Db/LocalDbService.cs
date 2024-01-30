using App.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Java.Text.Normalizer;

namespace App.Services.Db
{
    public class LocalDbService
    {
        private const string DbName = "Local.db3";
        private  SQLiteAsyncConnection _connection;
        public LocalDbService()
        {   

        }

        async Task Init()
        {
            if (_connection != null)
            {
                return;
            }
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DbName));
            var result = await _connection.CreateTableAsync<Note>();
        }

        public async Task<List<Note>> GetNotes()
        {
            if ( _connection == null )
            {
                await Init();
            }

            return await _connection.Table<Note>().ToListAsync();
        }

        public async Task<Note> GetNoteById(int id)
        {
            return await _connection.Table<Note>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateNote(Note note)
        {
            if (_connection == null)
            {
                await Init();
            }
            await _connection.InsertAsync(note);
        }

        public async Task Delete(int Id)
        {
            await _connection.ExecuteAsync("DELETE FROM Notes WHERE Id = ?", Id);

        }

    }
}

using App.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Db
{
    public class LocalDbService
    {
        private const string DbName = "Local.db3";
        private readonly SQLiteAsyncConnection _connection;
        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory,DbName));
            _connection.CreateTableAsync<Note>();
        }

        public async Task<List<Note>> GetNotes()
        {
            return await _connection.Table<Note>().ToListAsync();
        }

        public async Task<Note> GetNoteById(int id)
        {
            return await _connection.Table<Note>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateNote(Note note)
        {
             await _connection.InsertAsync(note);
        }

        public async Task Delete(Note note)
        {
            await _connection.DeleteAsync(note);
        }

    }
}

using App.Models;
using App.Services.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class NotesViewModel: BaseViewModel
    {
        private readonly LocalDbService _localDbService;

        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
        public Command AddMockNote { get; set; }
        public NotesViewModel(LocalDbService localDbService)
        {
            _localDbService = localDbService;
            AddMockNote = new Command(async () =>
            {
                await _localDbService.CreateNote(new Note { Content = "Mock Content", Name = "Mocked Name" });
            });
        }

        public async Task GetNotes()
        {
            try
            {
                var notes = await _localDbService.GetNotes();
                if(notes != null)
                {
                    foreach (var note in notes)
                    {
                        Notes.Add(note);
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}

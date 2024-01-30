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

        public ObservableCollection<NoteRow> Notes { get; set; } = new ObservableCollection<NoteRow>();
        public Command AddMockNote { get; set; }

        public Command GoToNote { get; set; }
        public NotesViewModel(LocalDbService localDbService): base()
        {
            _localDbService = localDbService;
            AddMockNote = new Command(async () =>
            {
                await _localDbService.CreateNote(new Note { Content = "Mock Content", Name = "Mocked Name", CreatedAt= DateTime.Now });
            });

            GoToNote = new Command<int>((Id)=>
            
                NavigateFromPageTo($"/Note?Id={Id}")
            );

            base.Commands = [AddMockNote, GoToNote];
        }

        public async Task GetNotes()
        {
            try
            {
                Notes.Clear();
                var notes = await _localDbService.GetNotes();
                if(notes != null)
                {
                    foreach (var note in notes)
                    {
                        Notes.Add(new NoteRow() { Id = note.Id, Content = note.Content, Name = note.Name, DisplayedTimeCreated = $"{note.CreatedAt.Day}.{note.CreatedAt.Month}.{note.CreatedAt.Year}" });
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

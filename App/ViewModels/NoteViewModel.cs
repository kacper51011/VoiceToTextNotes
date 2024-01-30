using App.Services.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class NoteViewModel: BaseViewModel, IQueryAttributable
    {
        LocalDbService _localDbService;
        private int noteId;

        public int NoteId
        {
            get { return noteId; }
            set
            {
                if (noteId != value)
                {
                    noteId = value;
                    OnPropertyChanged(nameof(NoteId));

                }
            }
        }

        public Command DeleteNote {  get; set; }
        
        public NoteViewModel(LocalDbService localDbService) : base()
        {
            _localDbService = localDbService;
            DeleteNote = new Command(async () =>
            {
                try
                {
                    bool value = await Shell.Current.DisplayAlert("Delete Note", "Are you sure you want to delete this note?", "Yes", "No");
                    if (value)
                    {
                        await _localDbService.Delete(NoteId);

                    }
                    await Shell.Current.DisplayAlert("Success", "successfully deleted the note", "OK");
                    await Task.Delay(1000);
                    NavigateFromPageTo("..");
                }
                catch (Exception)
                {

                    await Shell.Current.DisplayAlert("Failed", "Couldn`t delete the note, try again later", "OK");
                }

            });
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            {
                NoteId = int.Parse(query["Id"].ToString()!);
            }
        }
    }
}

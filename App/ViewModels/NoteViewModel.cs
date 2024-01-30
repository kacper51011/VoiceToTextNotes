using App.Services.Db;

namespace App.ViewModels
{
    public class NoteViewModel : BaseViewModel, IQueryAttributable
    {
        LocalDbService _localDbService;
        private int noteId;
        private string name;
        private string content;

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));

                }
            }
        }
        public string Content
        {
            get { return content; }
            set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged(nameof(Content));

                }
            }
        }
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

        public Command DeleteNote { get; set; }

        public NoteViewModel(LocalDbService localDbService) : base()
        {
            _localDbService = localDbService;
            DeleteNote = new Command(async () =>
            {
                try
                {
                    bool value = await Shell.Current.DisplayAlert("Delete Note", "Are you sure you want to delete this note?", "Yes", "No");
                    if (!value)
                    {
                        return;
                    }
                    if (value == true)
                    {
                        await _localDbService.Delete(NoteId);
                        await Shell.Current.DisplayAlert("Success", "successfully deleted the note", "OK");
                        await Task.Delay(1000);
                        NavigateFromPageTo("..");

                    }
                    
                }
                catch (Exception)
                {

                    await Shell.Current.DisplayAlert("Failed", "Couldn`t delete the note, try again later", "OK");
                }

            });
            base.Commands = [DeleteNote];

        }

        public async Task OnApearingAsync()
        {
            await GetNote();
        }
        private async Task GetNote()
        {
            try
            {

                var NoteItem = await _localDbService.GetNoteById(NoteId);
                Name = NoteItem.Name;
                Content = NoteItem.Content;
                return;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            try
            {
                NoteId = (int.Parse(query["Id"].ToString()!));
            }
            catch (Exception)
            {

                await Shell.Current.DisplayAlert("Something went wrong!", "Something unusual happened with the data, you will be redirected", "OK");
                await Task.Delay(1000);
                NavigateFromPageTo("..");
            }

        }


    }
}

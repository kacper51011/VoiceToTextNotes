using App.Models;
using CommunityToolkit.Maui.Views;

namespace App.Views;

public partial class CreateNoteFromVoicePopUp : Popup
{
	public CreateNoteFromVoicePopUp(string content)
	{
		InitializeComponent();
        ContentEditor.Text = content;
	}

    private void Accept_Clicked(object sender, EventArgs e)
    {
        var obj = new NoteInformation {Content = ContentEditor.Text, Name = NameEntry.Text };
        this.Close(obj);
    }

    private void Reject_Clicked(object sender, EventArgs e)
    {
        this.Close(null);
    }
}
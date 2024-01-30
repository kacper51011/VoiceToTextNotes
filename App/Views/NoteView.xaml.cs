using App.ViewModels;

namespace App.Views;

public partial class NoteView : ContentPage
{
	private NoteViewModel _viewModel;
	public NoteView(NoteViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewmodel;
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.NavigatedToPage();


    }
}
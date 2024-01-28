using App.ViewModels;

namespace App.Views;

public partial class NotesView : ContentPage
{
	private readonly NotesViewModel _viewModel;
	public NotesView(NotesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.NavigatedToPage();

		
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetNotes();
    }
}